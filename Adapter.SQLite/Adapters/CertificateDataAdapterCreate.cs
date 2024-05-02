using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SQLite.Adapters;

public class CertificateDataAdapterCreate(ICertDbContext dbContext) : ICertificateCreator
{
    public async Task<Certificate> CreateCertificate(Certificate certificate)
    {
        certificate.Issuer = await dbContext.Issuers.FirstOrDefaultAsync(i => i.Name == certificate.Issuer.Name)
                             ?? certificate.Issuer;
        certificate.CryptoAlgorithm =
            await dbContext.CryptoAlgorithms.FirstOrDefaultAsync(ca => ca.Name == certificate.CryptoAlgorithm.Name)
                                      ?? certificate.CryptoAlgorithm;

        if (certificate.SubjectAlternateNames != null)
        {
            var existingSans = await dbContext.SubjectAlternateNames.Where(c => certificate.SubjectAlternateNames.Select(s => s.Name).Contains(c.Name)).ToListAsync();
            certificate.SubjectAlternateNames = certificate.SubjectAlternateNames.Select(san => existingSans.FirstOrDefault(c => c.Name == san.Name) ?? san).ToList();
        }

        if (certificate.SystemNode != null)
        {
            var existingNodes = await dbContext.SystemNodes.Where(c => certificate.SystemNode.Select(s => s.Name).Contains(c.Name)).ToListAsync();
            certificate.SystemNode = certificate.SystemNode.Select(node => existingNodes.FirstOrDefault(c => c.Name == node.Name) ?? node).ToList();
        }

        certificate.IssuerId = certificate.Issuer.Id;
        certificate.CryptoAlgorithmId = certificate.CryptoAlgorithm.Id;

        dbContext.Certificates.Add(certificate);
        dbContext.SaveChanges();

        return certificate;
    }
}