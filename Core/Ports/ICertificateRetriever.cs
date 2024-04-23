using Core.Models;

public interface ICertificateRetriever
{
    Certificate GetCertificateById(long id);
    List<Certificate> GetAllCertificates();
    List<Certificate> GetCertificatesByDomain();
}

