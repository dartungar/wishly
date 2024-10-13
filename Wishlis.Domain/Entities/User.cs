namespace Wishlis.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly? Birthday { get; set; }
    public HashSet<Guid> FavoriteUserIds { get; set; }
}