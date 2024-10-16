namespace Wishlis.Application.DTO;

public record WishlistItemDto(int? Id, Guid userId, string Name, string Url, bool IsGroupGift);