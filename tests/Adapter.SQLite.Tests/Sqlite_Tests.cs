using Core.Models;
using Adapter.Api.SQLite.DataAccess;
using Adapter.SQLite.Adapters;

namespace Adapter.Api.SQLite.Tests;

public class DriveDbTests
{

    // private readonly IConfiguration _configuration;
    private readonly string _connectionString = "DataSource=:memory";
    // private readonly string _connectionString = "DataSource=../../../../../AppData/dev_database.db";
    private readonly ICertDbContext _dbContext;

    public DriveDbTests()
    {
        _dbContext = new CertDbContext(_connectionString);
        _dbContext.EnsureDatabaseCreated();

    }
    // Using tests to drive db before I've created any frontend or API endpoints
    [Fact]
    public async void CreateCertInSqliteDbTest()
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
        await adapter.CreateCertificate(cert);

    }

    [Fact]
    public async void GetCertFromSqliteDbTest()
    {
        Certificate newCert = new()
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
        var cert = await adapter.CreateCertificate(newCert);


        Assert.InRange(cert.Id, 1, long.MaxValue);

        long id = cert.Id;

        CertificateDataAdapterRetrieve retrievalAdapter = new(_dbContext);
        var retrievedCert = await retrievalAdapter.GetCertificateById(id)!;

        Assert.Equal(retrievedCert.Id, cert.Id);
        // Assert.Equal(retrievedCert.SubjectAlternateNames, cert.SubjectAlternateNames);
        // Assert.Equal(retrievedCert, cert);

    }

    [Fact]
    public async void InsertCertWithSan()
    {
        Certificate newCert = new()
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
        var cert = adapter.CreateCertificate(newCert);


        Assert.InRange(cert.Id, 1, long.MaxValue);

        long id = cert.Id;

        CertificateDataAdapterRetrieve retrievalAdapter = new(_dbContext);
        var retrievedCert = await retrievalAdapter.GetCertificateById(id)!;

        Assert.Equal(retrievedCert.Id, cert.Id);

    }

    [Fact]
    public async Task InsertCertAndMergeWithExistingSans()
    {
        // Build Two Certificates with Overlapping but Distinct SANs
        Certificate newCert1 = new()
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

        Certificate newCert2 = new()
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

        var cert1 = await adapter.CreateCertificate(newCert1);
        var cert2 =  await adapter.CreateCertificate(newCert2);

        Assert.InRange(cert1.Id, 1, long.MaxValue);
        Assert.InRange(cert2.Id, 1, long.MaxValue);


        //Retrieve the Newly Inserted Certificates
        CertificateDataAdapterRetrieve retrievalAdapter = new(_dbContext);
        var retrievedCert1 = await retrievalAdapter.GetCertificateById(cert1.Id);
        var retrievedCert2 = await retrievalAdapter.GetCertificateById(cert2.Id);

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
