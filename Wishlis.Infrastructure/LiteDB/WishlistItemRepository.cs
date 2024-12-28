using LiteDB.Async;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Interfaces;

namespace Wishlis.Infrastructure.LiteDB;

public class WishlistItemRepository : IWishlistItemRepository
{
    private readonly ILiteDbContext _dbContext;

    private ILiteCollectionAsync<WishlistItem> WishlistItems => _dbContext.Database.GetCollection<WishlistItem>(nameof(WishlistItem));
    
    public WishlistItemRepository(ILiteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    
    public async Task Create(WishlistItem item)
    {
        await WishlistItems.InsertAsync(item);
    }

    public async Task Save(WishlistItem item)
    {
        await WishlistItems.UpsertAsync(item);
    }

    public async Task BatchSave(IEnumerable<WishlistItem> items)
    {
        await WishlistItems.UpsertAsync(items);
    }

    public async Task Delete(Guid userId, Guid itemId)
    {
        await WishlistItems.DeleteAsync(itemId);
    }

    public async Task<IEnumerable<WishlistItem>> Get()
    {
        return await WishlistItems.FindAllAsync();
    }

    public async Task<IEnumerable<WishlistItem>> GetByUserId(Guid userId)
    {
        return await WishlistItems.Query().Where(x => x.UserId == userId).ToEnumerableAsync();
    }
}