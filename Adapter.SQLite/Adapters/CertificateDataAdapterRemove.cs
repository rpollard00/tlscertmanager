using Core.Ports;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SQLite.Adapters;

public class CertificateDataAdapterRemove(ICertDbContext dbContext) : ICertificateRemover
{
    public async Task RemoveCertificate(long id)
    {
        var cert = await dbContext.Certificates.Where(c => c.Id == id).FirstAsync();
        
        if (cert.Id == 0)
        {
            return;
        }
        
        dbContext.Certificates.Remove(cert);
        dbContext.SaveChanges();
    }
}