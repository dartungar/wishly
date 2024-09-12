using AutoMapper;
using Wishlis.Application.DTO;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Repositories;

namespace Wishlis.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<int> CreatePerson(PersonDto model)
    {
        return await _personRepository.Create(_mapper.Map<Person>(model));
    }

    public async Task DeletePerson(int id)
    {
        await _personRepository.Delete(id);
    }

    public async Task UpdatePerson(PersonDto model)
    {
        await _personRepository.Update(_mapper.Map<Person>(model));
    }

    public async Task<PersonDto> GetById(int id)
    {
        var person = await _personRepository.GetById(id);
        return _mapper.Map<PersonDto>(person);
    }

    public async Task<IEnumerable<PersonDto>> GetFavoritePersons(int ownerPersonId)
    {
        var favoritePersons = await _personRepository.GetFavoritePersons(ownerPersonId);
        return _mapper.Map<IEnumerable<PersonDto>>(favoritePersons);
    }

    public async Task AddPersonToFavorites(int favoritePersonId, int ownerPersonId)
    {
        await _personRepository.AddPersonToFavorites(favoritePersonId, ownerPersonId);
    }
}