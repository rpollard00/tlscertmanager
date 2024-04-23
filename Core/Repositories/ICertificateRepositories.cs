using Core.Models;

public interface ICertificateRepository
{
    Certificate GetById(long id);
    List<Certificate> GetAll();
    List<Certificate> GetByDomain(string domain);
    void Create(Certificate certificate);
    void Update(Certificate certificate);
    void Delete(long id);
}
