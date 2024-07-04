using Wishlis.Domain.Interfaces;

namespace Wishlis.Domain.Entities;

public class WishlistItem
{
    public int Id { get; set; }
    public Price Price { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int PersonId { get; set; }
    public bool IsGroupGift { get; set; }

    private readonly IWishlistItemRepository _wishlistItemRepository;
    
    public WishlistItem(IWishlistItemRepository wishlistItemRepository, int id, Price price, string name, string url, int personId, bool isGroupGift)
    {
        _wishlistItemRepository = wishlistItemRepository;
        Id = id;
        Price = price;
        Name = name;
        Url = url;
        PersonId = personId;
        IsGroupGift = isGroupGift;
    }

    public async Task Save()
    {
        await _wishlistItemRepository.Save(this);
    }
}