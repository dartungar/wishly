using Wishlis.Domain.Entities;

namespace Wishlis.Domain.Repositories;

public interface IWishlistItemRepository
{
    public Task Create(WishlistItem item);
    
    public Task Save(WishlistItem item);
    
    public Task Delete(Guid id);

    public Task<IEnumerable<WishlistItem>> Get();
    public Task<IEnumerable<WishlistItem>> GetByUserId(Guid userId);
}