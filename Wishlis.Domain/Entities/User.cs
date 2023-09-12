using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wishlis.Domain;

public class User : IDomainEntity
{
    public int Id { get; protected set; }
    
    public string Name { get; protected set; }
    
    public string PublicId { get; protected set; }
    
    public DateTime DateOfBirth { get; protected set; }
    
    public virtual UserSettings Settings { get; set; }
    
    public virtual ICollection<WishlistItem> Items { get; set; }
    
    public virtual ICollection<User> FavoriteUsers { get; set; }

    protected User() { }

    public User(int id, string name, string publicId, DateTime dateOfBirth)
    {
        Id = id;
        Name = name;
        PublicId = publicId;
        DateOfBirth = dateOfBirth;
    }
}