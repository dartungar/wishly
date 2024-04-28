using Microsoft.AspNetCore.Mvc;
using Wishlis.Application.DTO;
using Wishlis.Application.Services;
using Wishlis.Domain.Entities;

namespace Wishlis.Api.Controllers;

[ApiController]
[Route("persons")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly IPersonService _personService;
    private readonly IWishlistItemService _wishlistItemService;

    public PersonController(ILogger<PersonController> logger, IPersonService personService, IWishlistItemService wishlistItemService)
    {
        _logger = logger;
        _personService = personService;
        _wishlistItemService = wishlistItemService;
    }
    
    [HttpGet]
    public async Task<PersonDto> GetById(int id)
    {
        return await _personService.GetById(id);
    }
    
    [HttpPost]
    public async Task<int> CreatePerson([FromBody] PersonDto model)
    {
        return await _personService.CreatePerson(model);
    }
    
    [HttpPut]
    public async Task UpdatePerson([FromBody] PersonDto model)
    {
        await _personService.UpdatePerson(model);
    }


    [HttpGet("add-favorite-person")]
    public async Task AddPersonToFavorites(int favoritePersonId, int ownerPersonId)
    {
        await _personService.AddPersonToFavorites(favoritePersonId, ownerPersonId);
    }
    
    [HttpGet("wishlist")]
    public async Task<IEnumerable<WishlistItemDto>> GetByPersonId(int personId)
    {
        return await _wishlistItemService.GetByPersonId(personId);
    }
}