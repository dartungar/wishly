using Wishlis.Domain;

namespace Wishlis.Application.DTO;

public record WishlistItemDto(Guid Id, Guid UserId, string Name, double Price, Currency Currency,  string Url, bool IsGroupGift);