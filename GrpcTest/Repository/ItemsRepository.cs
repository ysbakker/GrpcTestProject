namespace GrpcTest.Repository;

public interface IItemsRepository
{
    Task<string[]> GetItemsAsync();
}

public class ItemsRepository : IItemsRepository
{
    public Task<string[]> GetItemsAsync()
    {
        return Task.FromResult(new[] { "Item 1", "Item 2" });
    }
}