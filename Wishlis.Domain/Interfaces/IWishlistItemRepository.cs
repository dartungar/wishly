using Wishlis.Domain.Entities;

namespace Wishlis.Domain.Interfaces;

public interface IWishlistItemRepository
{
    public Task Save(WishlistItem item);
    
    public Task Delete(int id);

    public Task<IEnumerable<WishlistItem>> Get();
    public Task<IEnumerable<WishlistItem>> GetByPersonId(int personId);
}