using Amazon.DynamoDBv2.DataModel;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Interfaces;

namespace Wishlis.Infrastructure.DynamoDB;

public class WishlistItemDynamoDbRepository : IWishlistItemRepository
{
    private readonly IDynamoDBContext _context;

    public WishlistItemDynamoDbRepository(IDynamoDBContext context)
    {
        _context = context;
    }
    
    public async Task Create(WishlistItem item)
    {
        await _context.SaveAsync(new WishlistItemDynamoDbModel(item));
    }

    public async Task Save(WishlistItem item)
    {
        await _context.SaveAsync(new WishlistItemDynamoDbModel(item));
    }

    public async Task Delete(Guid id)
    {
        await _context.DeleteAsync<WishlistItemDynamoDbModel>(id.ToString());
    }

    public async Task<IEnumerable<WishlistItem>> Get()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<WishlistItem>> GetByUserId(Guid userId)
    {
        var ddbModels =  await _context
            .QueryAsync<WishlistItemDynamoDbModel>(userId.ToString())
            .GetRemainingAsync();
        
        if (!ddbModels.Any())
            return new List<WishlistItem>();
        
        return ddbModels.Select(x => x.ToDomainModel());
    }
}