using Wishlis.Application.DTO;

namespace Wishlis.Application.Interfaces;

public interface IWishlistItemService
{
    Task Create(WishlistItemDto model);
    Task Update(WishlistItemDto model);
    Task Delete(int id);
    Task<IEnumerable<WishlistItemDto>> Get();
    Task<IEnumerable<WishlistItemDto>> GetByUserId(int userId);
}