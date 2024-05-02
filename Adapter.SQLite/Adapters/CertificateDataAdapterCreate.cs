using Core.Models;

namespace Adapter.SQLite.Adapters;

public class CertificateDataAdapterCreate(ICertDbContext dbContext) : ICertificateCreator
{
    public Certificate CreateCertificate(Certificate certificate)
    {
        certificate.Issuer = dbContext.Issuers.FirstOrDefault(i => i.Name == certificate.Issuer.Name)
                             ?? certificate.Issuer;
        certificate.CryptoAlgorithm = dbContext.CryptoAlgorithms.FirstOrDefault(ca => ca.Name == certificate.CryptoAlgorithm.Name)
                                      ?? certificate.CryptoAlgorithm;

        if (certificate.SubjectAlternateNames != null)
        {
            var existingSans = dbContext.SubjectAlternateNames.Where(c => certificate.SubjectAlternateNames.Select(s => s.Name).Contains(c.Name));
            certificate.SubjectAlternateNames = certificate.SubjectAlternateNames.Select(san => existingSans.FirstOrDefault(c => c.Name == san.Name) ?? san).ToList();
        }

        if (certificate.SystemNode != null)
        {
            var existingNodes = dbContext.SystemNodes.Where(c => certificate.SystemNode.Select(s => s.Name).Contains(c.Name));
            certificate.SystemNode = certificate.SystemNode.Select(node => existingNodes.FirstOrDefault(c => c.Name == node.Name) ?? node).ToList();
        }

        certificate.IssuerId = certificate.Issuer.Id;
        certificate.CryptoAlgorithmId = certificate.CryptoAlgorithm.Id;

        dbContext.Certificates.Add(certificate);
        dbContext.SaveChanges();

        return certificate;
    }
}