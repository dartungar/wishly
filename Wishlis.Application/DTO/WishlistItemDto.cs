namespace Wishlis.Application.DTO;

public record WishlistItemDto(int? Id, int PersonId, string Name, string Url, bool IsGroupGift);