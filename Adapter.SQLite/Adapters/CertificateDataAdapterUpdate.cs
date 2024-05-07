using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SQLite.Adapters;

public class CertificateDataAdapterUpdate(ICertDbContext dbContext) : ICertificateUpdater
{
    public async Task UpdateCertificate(Certificate certificate)
    {
        var certToUpdate = await dbContext.Certificates.Where(c => c.Id == certificate.Id)
            .Include(c => c.SubjectAlternateNames)
            .Include(c => c.SystemNode)
            .Include(c => c.Issuer)
            .FirstOrDefaultAsync();

        if (certToUpdate is { Id: 0 } or null)
        {
            return;
        }

        certToUpdate.SubjectName = certificate.SubjectName;
        certToUpdate.SystemNode = certificate.SystemNode;
        certToUpdate.SubjectAlternateNames = certificate.SubjectAlternateNames;
        certToUpdate.CryptoAlgorithm = certificate.CryptoAlgorithm;
        certToUpdate.Issuer = await UpdateIssuerOnChanged(certToUpdate, certificate);
        certToUpdate.IssueDate = certificate.IssueDate;
        certToUpdate.ExpirationDate = certificate.ExpirationDate;

        dbContext.SaveChanges();
    }

    private async Task<Issuer> UpdateIssuerOnChanged(Certificate existingCertificate, Certificate updatedCertificate)
    {
        if (existingCertificate.IssuerId == updatedCertificate.IssuerId)
        {
            return existingCertificate.Issuer;
        }

        var issuer = await dbContext.Issuers.FirstOrDefaultAsync(i => i.Name == updatedCertificate.Issuer.Name) ??
                     new Issuer { Name = updatedCertificate.Issuer.Name };

        return issuer;
    } 
}
