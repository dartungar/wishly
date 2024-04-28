using Wishlis.Application.DTO;

namespace Wishlis.Application.Services;

public interface IPersonService
{
    Task<int> CreatePerson(PersonDto model);
    Task UpdatePerson(PersonDto model);
    Task<PersonDto> GetById(int id);
    Task AddPersonToFavorites(int favoritePersonId, int ownerPersonId);
}