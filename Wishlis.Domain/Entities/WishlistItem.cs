namespace Wishlis.Domain;

public class WishlistItem : IDomainEntity
{
    public int Id { get; protected set; }
    public int UserId { get; protected set; }
    public string Name { get; protected set; }
    public string Url { get; protected set; }
    public decimal Cost { get; protected set; }
    public Currency Currency { get; protected set; }
}