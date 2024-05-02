using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Adapter.Api.SQLite;

public class CertificateDataAdapterRetrieve(ICertDbContext dbContext) : ICertificateRetriever
{
    public async Task<List<Certificate>> GetAllCertificates()
    {
        var certs = await dbContext.Certificates.Include(c => c.CryptoAlgorithm)
            .Include(c => c.SubjectAlternateNames)
            .Include(c => c.SystemNode)
            .Include(c => c.Issuer)
            .ToListAsync();

        return certs;
    }

    public async Task<Certificate>? GetCertificateById(long id)
    {
        var cert = await dbContext.Certificates.Where(c => c.Id == id)
            .Include(c => c.CryptoAlgorithm)
            .Include(c => c.SubjectAlternateNames)
            .Include(c => c.SystemNode)
            .Include(c => c.Issuer)
            .FirstOrDefaultAsync();


        return cert;


    }

    public async Task<List<Certificate>> GetCertificatesByDomain()
    {
        throw new NotImplementedException();
    }
}