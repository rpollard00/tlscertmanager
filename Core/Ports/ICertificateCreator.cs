using Core.Models;

public interface ICertificateCreator
{
    Task<Certificate> CreateCertificate(Certificate certificate);
}

