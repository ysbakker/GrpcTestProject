using Google.Protobuf.Collections;
using Grpc.Core;
using GrpcTest.Repository;
using Protos;

namespace GrpcTest.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly IItemsRepository _itemsRepository;

    public GreeterService(ILogger<GreeterService> logger, IItemsRepository itemsRepository)
    {
        _logger = logger;
        _itemsRepository = itemsRepository;
    }

    public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        var items = (await _itemsRepository.GetItemsAsync()).Select(item => new Item() { Message = item });
        var reply = new HelloReply();
        reply.Items.AddRange(items);
        return reply;
    }
}