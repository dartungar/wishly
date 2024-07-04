using Wishlis.Domain.Entities;
using Wishlis.Domain.Interfaces;

namespace Wishlis.Domain.Services;

public class WishlistItemService : IWishlistItemService
{
    private readonly IWishlistItemRepository _wishlistItemRepository;

    public WishlistItemService(IWishlistItemRepository wishlistItemRepository)
    {
        _wishlistItemRepository = wishlistItemRepository;
    }
    
    public async Task Save(WishlistItem item)
    {
        await item.Save();
    }
    
    public async Task Delete(int id)
    {
        await _wishlistItemRepository.Delete(id);
    }

    public async Task<IEnumerable<WishlistItem>> Get()
    {
        return await _wishlistItemRepository.Get();
    }

    public async Task<IEnumerable<WishlistItem>> GetByPersonId(int personId)
    {
        return await _wishlistItemRepository.GetByPersonId(personId);
    }
}