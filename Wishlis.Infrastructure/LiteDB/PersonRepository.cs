using LiteDB;
using LiteDB.Async;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Repositories;

namespace Wishlis.Infrastructure.LiteDB;

public class PersonRepository : IPersonRepository
{
    private readonly ILiteDbContext _dbContext;
    private ILiteCollectionAsync<Person> Persons => _dbContext.Database.GetCollection<Person>(nameof(Person));

    public PersonRepository(ILiteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> Create(Person person)
    {
        var inserted = await Persons.InsertAsync(person);
        return BsonMapper.Global.Deserialize<Person>(inserted).Id;
    }

    public Task Update(Person person)
    {
        return Persons.UpdateAsync(person);
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