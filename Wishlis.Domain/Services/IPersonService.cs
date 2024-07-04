using Wishlis.Domain.Entities;

namespace Wishlis.Domain.Services;

public interface IPersonService
{
    Task<Person> GetById(int id);
    Task<IEnumerable<Person>> GetFavoritePersons(int ownerPersonId);
    Task AddPersonToFavorites(int favoritePersonId, int ownerPersonId);
}