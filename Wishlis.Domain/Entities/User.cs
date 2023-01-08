namespace Wishlis.Domain;

public class User : IDomainEntity
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public string PublicId { get; protected set; }
    public List<string> ExternalIds { get; protected set; }
    public List<User> FavoriteUsers { get; set; }
}