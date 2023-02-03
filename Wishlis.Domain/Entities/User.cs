using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wishlis.Domain;

public class User : IDomainEntity
{
    [Column("id")]
    public int Id { get; protected set; }
    
    [Column("name")]
    public string Name { get; protected set; }
    
    [Column("public_id")]
    public string PublicId { get; protected set; }
    
    [Column("date_of_birth")]
    public DateTime DateOfBirth { get; protected set; }

    protected User() { }

    public User(int id, string name, string publicId, DateTime dateOfBirth)
    {
        Id = id;
        Name = name;
        PublicId = publicId;
        DateOfBirth = dateOfBirth;
    }
}