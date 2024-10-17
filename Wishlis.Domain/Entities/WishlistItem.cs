namespace Wishlis.Domain.Entities;

public class WishlistItem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string CurrencyCode { get; set; }
    public string Url { get; set; }
    public bool IsGroupGift { get; set; }
}