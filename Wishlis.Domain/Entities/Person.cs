namespace Wishlis.Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly? Birthday { get; set; }
    public HashSet<int> FavoritePersonIds { get; set; }
}