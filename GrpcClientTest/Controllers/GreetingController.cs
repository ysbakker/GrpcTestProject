using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Protos;

namespace GrpcClientTest.Controllers;

[ApiController]
[Route("[controller]")]
public class GreetingController : ControllerBase
{
    private readonly Greeter.GreeterClient _client;
    
    public GreetingController(Greeter.GreeterClient client)
    {
        _client = client;
    }

    [HttpGet]
    public ActionResult Get()
    {
        var result = _client.SayHello(new HelloRequest() {Name = "Hello there"}, new CallOptions());
        return Ok(result);
    }
}