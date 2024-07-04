using Wishlis.Domain.Interfaces;

namespace Wishlis.Domain.Entities;

public class Person
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public DateOnly? Birthday { get; set; }
    
    public HashSet<int> FavoritePersonIds { get; set; }

    public IEnumerable<WishlistItem> WishlistItems { get; } = new List<WishlistItem>();

    private readonly IPersonRepository _repository;
    
    public Person(IPersonRepository repository, int id, string name, DateOnly? birthday, HashSet<int> favoritePersonIds)
    {
        _repository = repository;
        Id = id;
        Name = name;
        Birthday = birthday;
        FavoritePersonIds = favoritePersonIds;
    }

    public async Task Save()
    {
        await _repository.Save(this);
    }
}