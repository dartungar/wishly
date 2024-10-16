using LiteDB.Async;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Repositories;

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

    public async Task Update(WishlistItem item)
    {
        await WishlistItems.UpdateAsync(item);
    }

    public async Task Delete(int id)
    {
        await WishlistItems.DeleteAsync(id);
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