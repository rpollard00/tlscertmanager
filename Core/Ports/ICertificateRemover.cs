namespace Core.Ports;

public interface ICertificateRemover
{
    Task RemoveCertificate(long id);
}