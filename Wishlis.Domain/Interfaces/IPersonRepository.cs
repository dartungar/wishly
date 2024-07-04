using Wishlis.Domain.Entities;

namespace Wishlis.Domain.Interfaces;

public interface IPersonRepository
{
    public Task Save(Person person);

    public Task<Person> GetById(int id);

    public Task AddPersonToFavorites(int favoritePersonId, int ownerPersonId);
    
    public Task<IEnumerable<Person>> GetFavoritePersons(int personId);
}