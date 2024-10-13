using Wishlis.Domain.Entities;

namespace Wishlis.Domain.Repositories;

public interface IWishlistItemRepository
{
    public Task Create(WishlistItem item);
    
    public Task Update(WishlistItem item);
    
    public Task Delete(int id);

    public Task<IEnumerable<WishlistItem>> Get();
    public Task<IEnumerable<WishlistItem>> GetByUserId(int userId);
}