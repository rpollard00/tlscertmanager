using Core.Dtos;

public interface ICertificateService
{
    CertificateDto CreateCertificate(CertificateDto cert);

    List<CertificateDto>? GetCertificates();
    CertificateDto? GetCertificateById(long id);

}
