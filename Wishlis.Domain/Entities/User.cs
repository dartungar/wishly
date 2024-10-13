namespace Wishlis.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly? Birthday { get; set; }
    public HashSet<int> FavoriteUserIds { get; set; }
}