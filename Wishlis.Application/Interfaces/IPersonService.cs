using Wishlis.Application.DTO;

namespace Wishlis.Application.Services;

public interface IPersonService
{
    Task<int> CreatePerson(PersonDto model);
    Task DeletePerson(int id);
    Task UpdatePerson(PersonDto model);
    Task<PersonDto> GetById(int id);
    Task<IEnumerable<PersonDto>> GetFavoritePersons(int ownerPersonId);
    Task AddPersonToFavorites(int favoritePersonId, int ownerPersonId);
}