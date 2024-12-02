using Wishlis.Application.DTO;
using Wishlis.Domain.Entities;

namespace Wishlis.Application.Mappers;

public static class WishlistItemMappingExtensions
{
    public static WishlistItemDto ToWishlistItemDto(this WishlistItem item)
        => new WishlistItemDto(item.Id, item.UserId, item.Name, item.Price, item.CurrencyCode, item.Url,
            item.IsGroupGift);

    public static WishlistItem ToWishlistItem(this WishlistItemDto dto) => new()
    {
        Id = dto.Id,
        UserId = dto.UserId,
        Name = dto.Name,
        CurrencyCode = dto.CurrencyCode,
        Price = dto.Price,
        Url = dto.Url,
    };
}