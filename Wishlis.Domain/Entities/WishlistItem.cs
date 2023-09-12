using System.ComponentModel.DataAnnotations;

namespace Wishlis.Domain;

public class WishlistItem : IDomainEntity
{
    public int Id { get; protected set; }
    public int UserId { get; protected set; }
    public string Name { get; protected set; }
    public string Url { get; protected set; }
    public decimal Cost { get; protected set; }
    public bool IsJointPurchase { get; set; }
    
    public virtual Currency Currency { get; protected set; }
}