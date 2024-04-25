using Core.Models;
using Core.Dtos;
using Core.Mapper;

namespace Adapter.Api.Adapter;

public class ApiAdapter : ICertificateService
{

    private readonly ICertificateCreator _certificateCreator;
    private readonly ICertificateRemover _certificateRemover;
    private readonly ICertificateRetriever _certificateRetriever;
    private readonly ICertificateUpdater _certificateUpdater;

    public ApiAdapter(ICertificateCreator certificateCreator,
                      ICertificateRemover certificateRemover,
                      ICertificateRetriever certificateRetriever,
                      ICertificateUpdater certificateUpdater)
    {

        _certificateCreator = certificateCreator;
        _certificateRemover = certificateRemover;
        _certificateRetriever = certificateRetriever;
        _certificateUpdater = certificateUpdater;
    }

    public CertificateDto CreateCertificate(CertificateDto cert)
    {
        // _certificateCreator.CreateCertificate(cert);
        throw new NotImplementedException("Todo Create Certificate Adapter");
    }

    public CertificateDto? GetCertificateById(long id)
    {

        throw new NotImplementedException("Todo GetCertificate By Id");
        // return CertificateDtoMapper.CertificateToDto();
    }

    public List<CertificateDto>? GetCertificates()
    {
        var result = _certificateRetriever.GetAllCertificates();

        if (result.Count == 0)
        {
            return null;
        }

        List<CertificateDto> output = new();
        result.ForEach(r => output.Add(CertificateDtoMapper.CertificateToDto(r)));

        return output;

    }

}
