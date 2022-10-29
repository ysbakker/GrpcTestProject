using FluentAssertions;
using Grpc.Core;
using GrpcTest.Repository;
using GrpcTest.Services;
using Microsoft.Extensions.Logging;
using Grpc.Core.Testing;
using Moq;
using Protos;

namespace GrpcTest.Tests;

public class GreeterServiceTest
{
    private readonly Mock<ILogger<GreeterService>> _loggerMock = new();
    private readonly Mock<IItemsRepository> _itemsRepositoryMock = new();
    private readonly GreeterService _sut;


    public GreeterServiceTest()
    {
        _sut = new GreeterService(_loggerMock.Object, _itemsRepositoryMock.Object);
    }

    [Fact]
    public async Task SayHello_GetsAndReturnsItems()
    {
        var request = new HelloRequest();
        _itemsRepositoryMock.Setup(m => m.GetItemsAsync()).ReturnsAsync(new[] { "Test1", "Test2" });
        var context = TestServerCallContext.Create(
            method: nameof(GreeterService.SayHello)
            , host: "localhost"
            , deadline: DateTime.Now.AddMinutes(30)
            , requestHeaders: new Metadata()
            , cancellationToken: CancellationToken.None
            , peer: "10.0.0.25:5001"
            , authContext: null
            , contextPropagationToken: null
            , writeHeadersFunc: _ => Task.CompletedTask
            , writeOptionsGetter: () => new WriteOptions()
            , writeOptionsSetter: _ => { }
        );

        var result = await _sut.SayHello(request, context);

        result.Items.Should().HaveCount(2);
    }
}