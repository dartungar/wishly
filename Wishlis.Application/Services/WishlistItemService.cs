using Wishlis.Application.DTO;
using Wishlis.Application.Interfaces;
using Wishlis.Application.Mappers;
using Wishlis.Domain.Interfaces;

namespace Wishlis.Application.Services;

public class WishlistItemService : IWishlistItemService
{
    private readonly IWishlistItemRepository _wishlistItemRepository;

    public WishlistItemService(IWishlistItemRepository wishlistItemRepository)
    {
        _wishlistItemRepository = wishlistItemRepository;
    }

    public async Task Create(WishlistItemDto model)
    {
        await _wishlistItemRepository.Create(model.ToWishlistItem());
    }
    
    public async Task Save(WishlistItemDto model)
    {
        await _wishlistItemRepository.Save(model.ToWishlistItem());
    }
    
    public async Task Delete(Guid userId, Guid itemId)
    {
        await _wishlistItemRepository.Delete(userId, itemId);
    }

    public async Task<IEnumerable<WishlistItemDto>> Get()
    {
        var items = await _wishlistItemRepository.Get();
        return items.Select(x => x.ToWishlistItemDto());
    }

    public async Task<IEnumerable<WishlistItemDto>> GetByUserId(Guid userId)
    {
        var items = await _wishlistItemRepository.GetByUserId(userId);
        return items.Select(x => x.ToWishlistItemDto());
    }
}