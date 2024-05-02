using Core.Models;

public interface ICertificateRetriever
{
    Task<Certificate>? GetCertificateById(long id);
    Task<List<Certificate>> GetAllCertificates();
    Task<List<Certificate>> GetCertificatesByDomain();
}

