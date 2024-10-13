namespace Wishlis.Application.DTO;

public record WishlistItemDto(int? Id, int UserId, string Name, string Url, bool IsGroupGift);