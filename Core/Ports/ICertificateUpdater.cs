using Core.Models;

public interface ICertificateUpdater
{
    Task UpdateCertificate(Certificate certificate);
}

