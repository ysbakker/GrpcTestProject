using FluentAssertions;
using Grpc.Core;
using GrpcClientTest.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Protos;

namespace GrpcClientTest.Tests;

public class GreetingControllerTest
{
    private readonly GreetingController _sut;
    private readonly Mock<Greeter.GreeterClient> _greeterClientMock = new();
    
    public GreetingControllerTest()
    {
        _sut = new GreetingController(_greeterClientMock.Object);
    }

    [Fact]
    public async Task Get_CallsGrpcClient()
    {
        HelloReply reply = new HelloReply();
        _greeterClientMock
            .Setup(m => m.SayHello(It.IsAny<HelloRequest>(), It.IsAny<CallOptions>()))
            .Returns(reply);
        
        var result = _sut.Get();

        result.Should().BeOfType<OkObjectResult>();
        ((OkObjectResult)result).Value.Should().BeOfType<HelloReply>().And.Be(reply);
    } 
}