using Wishlis.Application.DTO;

namespace Wishlis.Application.Interfaces;

public interface IWishlistItemService
{
    Task Create(WishlistItemDto model);
    Task Save(WishlistItemDto model);
    Task Delete(Guid id);
    Task<IEnumerable<WishlistItemDto>> Get();
    Task<IEnumerable<WishlistItemDto>> GetByUserId(Guid userId);
}