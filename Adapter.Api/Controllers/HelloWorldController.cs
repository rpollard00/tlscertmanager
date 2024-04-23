using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class HelloWorldController : ControllerBase
{

    [HttpGet(Name = "GetHelloWorld")]
    public HelloWorldObject GetHelloWorld()
    {
        return new HelloWorldObject
        {
            Name = "Hello World"
        };
    }
}

public class HelloWorldObject
{
    public string Name { get; set; } = "Hello World";
}
