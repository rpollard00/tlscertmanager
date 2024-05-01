using Core.Models;

public interface ICertificateRetriever
{
    Certificate? GetCertificateById(long id);
    Task<List<Certificate>> GetAllCertificates();
    List<Certificate> GetCertificatesByDomain();
}

