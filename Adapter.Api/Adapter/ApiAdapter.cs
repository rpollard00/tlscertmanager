using Core.Models;

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

    public Certificate CreateCertificate(Certificate cert) => _certificateCreator.CreateCertificate(cert);

    public Certificate? GetCertificateById(long id) => _certificateRetriever.GetCertificateById(id);

    public List<Certificate> GetCertificates() => _certificateRetriever.GetAllCertificates();
}
