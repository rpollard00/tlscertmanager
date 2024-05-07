using Core.Dtos;

public interface ICertificateService
{
    Task<CertificateDto> CreateCertificate(CertificateDto cert);

    Task<List<CertificateDto?>?> GetCertificates();
    Task<CertificateDto>? GetCertificateById(long id);

}
