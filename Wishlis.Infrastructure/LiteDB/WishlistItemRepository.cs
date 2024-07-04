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
    
    public async Task Save(WishlistItem item)
    {
        await WishlistItems.UpsertAsync(item);
    }

    public async Task Delete(int id)
    {
        await WishlistItems.DeleteAsync(id);
    }

    public async Task<IEnumerable<WishlistItem>> Get()
    {
        return await WishlistItems.FindAllAsync();
    }

    public async Task<IEnumerable<WishlistItem>> GetByPersonId(int personId)
    {
        return await WishlistItems.Query().Where(x => x.PersonId == personId).ToEnumerableAsync();
    }
}