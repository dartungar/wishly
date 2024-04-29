using Microsoft.AspNetCore.Mvc;
using Wishlis.Application.DTO;
using Wishlis.Application.Services;
using Wishlis.Domain.Entities;

namespace Wishlis.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly ILogger<PersonsController> _logger;
    private readonly IPersonService _personService;
    private readonly IWishlistItemService _wishlistItemService;

    public PersonsController(ILogger<PersonsController> logger, IPersonService personService, IWishlistItemService wishlistItemService)
    {
        _logger = logger;
        _personService = personService;
        _wishlistItemService = wishlistItemService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetById(int id)
    {
        return await _personService.GetById(id);
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> CreatePerson(PersonDto model)
    {
        return await _personService.CreatePerson(model);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(PersonDto model)
    {
        await _personService.UpdatePerson(model);
        return Ok();
    }


    [HttpGet("{ownerPersonId}/add-favorite-person")]
    public async Task<IActionResult> AddPersonToFavorites(int favoritePersonId, int ownerPersonId)
    {
        await _personService.AddPersonToFavorites(favoritePersonId, ownerPersonId);
        return Ok();
    }
    
    [HttpGet("{personId}/wishlist")]
    public async Task<ActionResult<IEnumerable<WishlistItemDto>>> GetPersonWithlist(int personId)
    {
        var result = await _wishlistItemService.GetByPersonId(personId);
        return Ok(result);
    }
}