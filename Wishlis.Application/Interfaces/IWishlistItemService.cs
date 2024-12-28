using Wishlis.Application.DTO;

namespace Wishlis.Application.Interfaces;

public interface IWishlistItemService
{
    Task Create(WishlistItemDto model);
    Task Save(WishlistItemDto model);
    Task Delete(Guid userId, Guid itemId);
    Task<IEnumerable<WishlistItemDto>> Get();
    Task<IEnumerable<WishlistItemDto>> GetByUserId(Guid userId);
    Task UpdateCurrencyForAllUserItems(string currencyCode, Guid userId);
}