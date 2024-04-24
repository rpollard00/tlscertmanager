using Core.Models;
using Adapter.Api.SQLite.DataAccess;

namespace Adapter.Api.SQLite.Tests;

public class DriveDbTests
{

    // private readonly IConfiguration _configuration;
    private readonly string _connectionString = "DataSource=:memory";
    private readonly ICertDbContext _dbContext;

    public DriveDbTests()
    {
        _dbContext = new CertDbContext(_connectionString);
        _dbContext.EnsureDatabaseCreated();

    }
    // Using tests to drive db before I've created any frontend or API endpoints
    [Fact]
    public void CreateCertInSqliteDbTest()
    {
        Certificate cert = new()
        {
            SubjectName = "load.test.com",
            IssueDate = 1682273316000,
            ExpirationDate = 1745431716000,

            CryptoAlgorithm = new()
            {
                Name = "ECDSA with SHA-384",
            },
            Issuer = new()
            {
                Name = "Let's Encrypt",
            },
        };

        CertificateDataAdapterCreate adapter = new(_dbContext);
        adapter.CreateCertificate(cert);

    }

    [Fact]
    public void GetCertFromSqliteDbTest()
    {
        Certificate cert = new()
        {
            SubjectName = "testCert.test.com",
            IssueDate = 1682273316000,
            ExpirationDate = 1745431716000,

            CryptoAlgorithm = new()
            {
                Name = "ECDSA with SHA-384",
            },
            Issuer = new()
            {
                Name = "Let's Encrypt",
            },
        };

        CertificateDataAdapterCreate adapter = new(_dbContext);
        cert = adapter.CreateCertificate(cert);


        Assert.InRange(cert.Id, 1, long.MaxValue);

        long id = cert.Id;

        CertificateDataAdapterRetrieve retrievalAdapter = new(_dbContext);
        var retrievedCert = retrievalAdapter.GetCertificateById(id);
        if (retrievedCert == null)
        {
            Assert.Fail("Failed to retrieve certificate");
        }

        Assert.Equal(retrievedCert.Id, cert.Id);
        // Assert.Equal(retrievedCert.SubjectAlternateNames, cert.SubjectAlternateNames);
        // Assert.Equal(retrievedCert, cert);

    }

    [Fact]
    public void InsertCertWithSan()
    {
        Certificate cert = new()
        {
            SubjectName = "sanCert.test.com",
            IssueDate = 1682273316000,
            ExpirationDate = 1745431716000,

            SubjectAlternateNames = new() {
                { new SubjectAlternateName { Name = "otherdomain.test.local"}},
                { new SubjectAlternateName { Name = "otherdomain2.test.local"}},
                { new SubjectAlternateName { Name = "otherdomain3.test.local"}},
            },
            CryptoAlgorithm = new()
            {
                Name = "SHA-256 with RSA Encryption",
            },
            Issuer = new()
            {
                Name = "DigiCert Inc",
            },
        };

        CertificateDataAdapterCreate adapter = new(_dbContext);
        cert = adapter.CreateCertificate(cert);


        Assert.InRange(cert.Id, 1, long.MaxValue);

        long id = cert.Id;

        CertificateDataAdapterRetrieve retrievalAdapter = new(_dbContext);
        var retrievedCert = retrievalAdapter.GetCertificateById(id);
        if (retrievedCert == null)
        {
            Assert.Fail("Failed to retrieve certificate");
        }

        Assert.Equal(retrievedCert.Id, cert.Id);
        // Assert.Equal(retrievedCert.SubjectAlternateNames, cert.SubjectAlternateNames);
        // Assert.Equal(retrievedCert, cert);

    }

    [Fact]
    public void InsertCertAndMergeWithExistingSans()
    {
        // Build Two Certificates with Overlapping but Distinct SANs
        Certificate cert1 = new()
        {
            SubjectName = "sanCert.test.com",
            IssueDate = 1682273316000,
            ExpirationDate = 1745431716000,

            SubjectAlternateNames = new() {
                { new SubjectAlternateName { Name = "otherdomain.test.local"}},
                { new SubjectAlternateName { Name = "otherdomain2.test.local"}},
                { new SubjectAlternateName { Name = "otherdomain3.test.local"}},
            },
            CryptoAlgorithm = new()
            {
                Name = "SHA-256 with RSA Encryption",
            },
            Issuer = new()
            {
                Name = "DigiCert Inc",
            },
        };

        Certificate cert2 = new()
        {
            SubjectName = "sanCertMerge.test.com",
            IssueDate = 1682273316000,
            ExpirationDate = 1745431716000,

            SubjectAlternateNames = new() {
                { new SubjectAlternateName { Name = "otherdomain2.test.local"}},
                { new SubjectAlternateName { Name = "otherdomain3.test.local"}},
                { new SubjectAlternateName { Name = "otherdomain4.test.local"}},
            },
            CryptoAlgorithm = new()
            {
                Name = "SHA-256 with RSA Encryption",
            },
            Issuer = new()
            {
                Name = "DigiCert Inc",
            },
        };

        CertificateDataAdapterCreate adapter = new(_dbContext);

        cert1 = adapter.CreateCertificate(cert1);
        cert2 = adapter.CreateCertificate(cert2);

        Assert.InRange(cert1.Id, 1, long.MaxValue);
        Assert.InRange(cert2.Id, 1, long.MaxValue);


        //Retrieve the Newly Inserted Certificates
        CertificateDataAdapterRetrieve retrievalAdapter = new(_dbContext);
        var retrievedCert1 = retrievalAdapter.GetCertificateById(cert1.Id);
        var retrievedCert2 = retrievalAdapter.GetCertificateById(cert2.Id);

        // Assert that the retrieved certificates have the correct SubjectAlternateNames
        Assert.Equal(3, retrievedCert1.SubjectAlternateNames.Count);
        Assert.Contains("otherdomain.test.local", retrievedCert1.SubjectAlternateNames.Select(san => san.Name));
        Assert.Contains("otherdomain2.test.local", retrievedCert1.SubjectAlternateNames.Select(san => san.Name));
        Assert.Contains("otherdomain3.test.local", retrievedCert1.SubjectAlternateNames.Select(san => san.Name));

        Assert.Equal(3, retrievedCert2.SubjectAlternateNames.Count);
        Assert.Contains("otherdomain2.test.local", retrievedCert2.SubjectAlternateNames.Select(san => san.Name));
        Assert.Contains("otherdomain3.test.local", retrievedCert2.SubjectAlternateNames.Select(san => san.Name));
        Assert.Contains("otherdomain4.test.local", retrievedCert2.SubjectAlternateNames.Select(san => san.Name));

        // Assert that the SANs are properly merged and not duplicated
        var allSanNames = retrievedCert1.SubjectAlternateNames.Select(san => san.Name)
            .Concat(retrievedCert2.SubjectAlternateNames.Select(san => san.Name))
            .Distinct()
            .ToList();

        Assert.Equal(4, allSanNames.Count);
        Assert.Contains("otherdomain.test.local", allSanNames);
        Assert.Contains("otherdomain2.test.local", allSanNames);
        Assert.Contains("otherdomain3.test.local", allSanNames);
        Assert.Contains("otherdomain4.test.local", allSanNames);

    }
}
