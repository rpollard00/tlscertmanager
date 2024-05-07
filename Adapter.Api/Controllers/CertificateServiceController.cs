using Core.Dtos;
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
        try
        {
            var result = _certSvc.GetCertificates();
            return Ok(result);
        }
        catch
        {
            return BadRequest();
        }
    }


    [HttpGet("{id}")]
    public string GetCertificateById(long id)
    {
        return $"Does this thing work {id}";
    }

    [HttpPost()]
    public IActionResult PostNewCertificate(CertificateDto dto)
    {
        var result = _certSvc.CreateCertificate(dto);
        return Ok(result);
    }
}
