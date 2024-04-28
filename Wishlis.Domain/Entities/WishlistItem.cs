namespace Wishlis.Domain.Entities;

public class WishlistItem
{
    public int Id { get; set; }
    public Price Price { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int PersonId { get; set; }
    public bool IsGroupGift { get; set; }
}