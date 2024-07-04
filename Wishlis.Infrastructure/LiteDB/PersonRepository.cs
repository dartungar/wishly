using LiteDB;
using LiteDB.Async;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Interfaces;

namespace Wishlis.Infrastructure.LiteDB;

public class PersonRepository : IPersonRepository
{
    private readonly ILiteDbContext _dbContext;
    private ILiteCollectionAsync<Person> Persons => _dbContext.Database.GetCollection<Person>(nameof(Person));

    public PersonRepository(ILiteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task Save(Person person)
    {
        return Persons.UpsertAsync(person);
    }

    public Task<Person> GetById(int id)
    {
        return Persons.FindByIdAsync(id);
    }

    public async Task AddPersonToFavorites(int favoritePersonId, int ownerPersonId)
    {
        var person = await Persons.FindByIdAsync(ownerPersonId);
        person.FavoritePersonIds.Add(favoritePersonId);
        await Persons.UpdateAsync(person);
    }

    public async Task<IEnumerable<Person>> GetFavoritePersons(int personId)
    {
        var person = await Persons.FindByIdAsync(personId);
        return await Persons.Query().Where(x => person.FavoritePersonIds.Contains(x.Id)).ToEnumerableAsync();
    }
}