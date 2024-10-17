using Wishlis.Domain;

namespace Wishlis.Application.DTO;

public record WishlistItemDto(
    Guid Id,
    Guid UserId,
    string Name,
    double Price,
    string CurrencyCode,
    string Url,
    bool IsGroupGift);