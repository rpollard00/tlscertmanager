using Core.Ports;

namespace Adapter.SQLite;

public class CertificateDataAdapterRemove(ICertDbContext dbContext) : ICertificateRemover
{
    private readonly ICertDbContext _dbContext = dbContext;

    public async Task RemoveCertificate(long id)
    {
    }
}