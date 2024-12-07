namespace Wishlis.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly? Birthday { get; set; }
    public string? CurrencyCode { get; set; } = "USD";
    public bool? IsProfileSearchable { get; set; } = true;
    public HashSet<Guid> FavoriteUserIds { get; set; } = new();
}