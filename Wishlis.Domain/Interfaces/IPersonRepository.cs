using Wishlis.Domain.Entities;

namespace Wishlis.Domain.Repositories;

public interface IPersonRepository
{
    public Task<int> Create(Person person);
    
    public Task Update(Person person);

    public Task<Person> GetById(int id);

    public Task AddPersonToFavorites(int favoritePersonId, int ownerPersonId);
    
    public Task<IEnumerable<Person>> GetFavoritePersons(int personId);
}