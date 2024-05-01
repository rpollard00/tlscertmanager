using Core.Dtos;

public interface ICertificateService
{
    CertificateDto CreateCertificate(CertificateDto cert);

    Task<List<CertificateDto>>? GetCertificates();
    CertificateDto? GetCertificateById(long id);

}
