using Adapter.Api.SQLite.DataAccess;
using Core;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Adapter.Api.SQLite;

public class CertificateDataAdapterCreate : ICertificateCreator
{
    private readonly ICertDbContext _dbContext;

    public CertificateDataAdapterCreate(ICertDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Certificate CreateCertificate(Certificate certificate)
    {
        certificate.Issuer = _dbContext.Issuers.FirstOrDefault(i => i.Name == certificate.Issuer.Name)
            ?? certificate.Issuer;
        certificate.CryptoAlgorithm = _dbContext.CryptoAlgorithms.FirstOrDefault(ca => ca.Name == certificate.CryptoAlgorithm.Name)
            ?? certificate.CryptoAlgorithm;

        if (certificate.SubjectAlternateNames != null)
        {
            var existingSans = _dbContext.SubjectAlternateNames.Where(c => certificate.SubjectAlternateNames.Select(s => s.Name).Contains(c.Name));
            certificate.SubjectAlternateNames = certificate.SubjectAlternateNames.Select(san => existingSans.FirstOrDefault(c => c.Name == san.Name) ?? san).ToList();
        }

        if (certificate.SystemNode != null)
        {
            var existingNodes = _dbContext.SystemNodes.Where(c => certificate.SystemNode.Select(s => s.Name).Contains(c.Name));
            certificate.SystemNode = certificate.SystemNode.Select(node => existingNodes.FirstOrDefault(c => c.Name == node.Name) ?? node).ToList();
        }

        certificate.IssuerId = certificate.Issuer.Id;
        certificate.CryptoAlgorithmId = certificate.CryptoAlgorithm.Id;

        _dbContext.Certificates.Add(certificate);
        _dbContext.SaveChanges();

        return certificate;
    }
}

public class CertificateDataAdapterRetrieve : ICertificateRetriever
{
    private readonly ICertDbContext _dbContext;

    public CertificateDataAdapterRetrieve(ICertDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Certificate> GetAllCertificates()
    {
        throw new NotImplementedException();
    }

    public Certificate? GetCertificateById(long id)
    {
        var cert = _dbContext.Certificates.Where(c => c.Id == id)
                                  .Include(c => c.CryptoAlgorithm)
                                  .Include(c => c.SubjectAlternateNames)
                                  .Include(c => c.SystemNode)
                                  .Include(c => c.Issuer)
                                  .FirstOrDefault();

        return cert;


    }

    public List<Certificate> GetCertificatesByDomain()
    {
        throw new NotImplementedException();
    }
}

public class CertificateDataAdapterRemove : ICertificateRemover
{
    private readonly ICertDbContext _dbContext;

    public CertificateDataAdapterRemove(ICertDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void RemoveCertificate(long id)
    {
    }
}
