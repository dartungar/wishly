using Wishlis.Domain.Entities;
using Wishlis.Domain.Interfaces;

namespace Wishlis.Domain.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    public async Task<Person> GetById(int id)
    {
        return await _personRepository.GetById(id);
    }

    public async Task<IEnumerable<Person>> GetFavoritePersons(int ownerPersonId)
    {
        return await _personRepository.GetFavoritePersons(ownerPersonId);
    }

    public async Task AddPersonToFavorites(int favoritePersonId, int ownerPersonId)
    {
        await _personRepository.AddPersonToFavorites(favoritePersonId, ownerPersonId);
    }
}