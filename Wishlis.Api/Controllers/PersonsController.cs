using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wishlis.Application.DTO;
using Wishlis.Application.Services;
using Wishlis.Domain;
using Microsoft.AspNetCore.Http;

namespace Wishlis.Api.Controllers;

/// <summary>
/// 
/// </summary>
[ApiVersion(1)]
[Route("/api/v{version:apiVersion}/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly ILogger<PersonsController> _logger;
    private readonly IPersonService _personService;
    private readonly IWishlistItemService _wishlistItemService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="personService"></param>
    /// <param name="wishlistItemService"></param>
    public PersonsController(ILogger<PersonsController> logger, IPersonService personService,
        IWishlistItemService wishlistItemService)
    {
        _logger = logger;
        _personService = personService;
        _wishlistItemService = wishlistItemService;
    }

    /// <summary>
    /// Get a person by ID.
    /// </summary>
    /// <param name="id">person's ID.</param>
    /// <returns>A person object.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetById(int id)
    {
        return Ok(await _personService.GetById(id));
    }

    /// <summary>
    /// Create a new person.
    /// </summary>
    /// <param name="model">The person's data.</param>
    /// <returns>Created person's ID.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize]
    public async Task<ActionResult<int>> CreatePerson(PersonDto model)
    {
        return Created("", await _personService.CreatePerson(model));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize(Roles = UserGroups.Admins)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeletePerson(int id)
    {
        await _personService.DeletePerson(id);
        return NoContent();
    }

    /// <summary>
    /// Update an existing person.
    /// </summary>
    /// <param name="model">Updated data.</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdatePerson(PersonDto model)
    {
        await _personService.UpdatePerson(model);
        return NoContent();
    }

    /// <summary>
    /// Get a list of a person's favorite people.
    /// </summary>
    /// <param name="ownerPersonId">ID of the person.</param>
    /// <returns></returns>
    [HttpGet("{ownerPersonId}/favorite-persons")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetFavoritePersons(int ownerPersonId)
    {
        var persons = await _personService.GetFavoritePersons(ownerPersonId);
        return Ok(persons);
    }

    /// <summary>
    /// Add a new person as a favorite to another ('owner') person.
    /// </summary>
    /// <param name="favoritePersonId">ID of the person that will be added as a favorite.</param>
    /// <param name="ownerPersonId">ID of the person, to whom a favorite person will be added.</param>
    /// <returns></returns>
    [HttpPost("{ownerPersonId}/favorite-persons")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AddPersonToFavorites(int favoritePersonId, int ownerPersonId)
    {
        await _personService.AddPersonToFavorites(favoritePersonId, ownerPersonId);
        return NoContent();
    }

    /// <summary>
    /// Get a list of person's wishlist items.
    /// </summary>
    /// <param name="personId">ID of the person.</param>
    /// <returns>List of items.</returns>
    [HttpGet("{personId}/items")]
    public async Task<ActionResult<IEnumerable<WishlistItemDto>>> GetPersonWishlist(int personId)
    {
        var result = await _wishlistItemService.GetByPersonId(personId);
        return Ok(result);
    }
}