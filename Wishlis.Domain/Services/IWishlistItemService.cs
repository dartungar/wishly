using Wishlis.Domain.Entities;

namespace Wishlis.Domain.Services;

public interface IWishlistItemService
{
    Task Save(WishlistItem item);
    
    Task Delete(int id);
    
    Task<IEnumerable<WishlistItem>> Get();
    
    Task<IEnumerable<WishlistItem>> GetByPersonId(int personId);
}