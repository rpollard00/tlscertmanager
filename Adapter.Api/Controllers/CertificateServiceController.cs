using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CertificateServiceController : ControllerBase
{
    private readonly ICertificateService _certSvc;

    public CertificateServiceController(ICertificateService certSvc)
    {
        _certSvc = certSvc;
    }

    [HttpGet()]
    public IActionResult GetAllCertficiates()
    {
        var result = _certSvc.GetCertificates();

        return Ok(result);
    }


    [HttpGet("{id}")]
    public string GetCertificateById(long id)
    {
        return $"Does this thing work {id}";
    }

    [HttpPost()]
    public string PostNewCertificate()
    {
        return $"There must be some way to get the body of the request from the middleware";
    }
}
