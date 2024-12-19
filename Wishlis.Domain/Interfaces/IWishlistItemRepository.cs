using Wishlis.Domain.Entities;

namespace Wishlis.Domain.Interfaces;

public interface IWishlistItemRepository
{
    public Task Create(WishlistItem item);
    
    public Task Save(WishlistItem item);
    
    public Task Delete(Guid userId, Guid itemId);

    public Task<IEnumerable<WishlistItem>> Get();
    public Task<IEnumerable<WishlistItem>> GetByUserId(Guid userId);
}