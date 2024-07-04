using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Services;

namespace Wishlis.Api.Controllers;

[ApiVersion(1)]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly ILogger<PersonsController> _logger;
    private readonly IPersonService _personService;
    private readonly IWishlistItemService _wishlistItemService;

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
    public async Task<ActionResult<Person>> GetById(int id)
    {
        return await _personService.GetById(id);
    }

    /// <summary>
    /// Create a new person.
    /// </summary>
    /// <param name="model">The person's data.</param>
    /// <returns>Created person's ID.</returns>
    [HttpPost]
    public async Task<ActionResult<int>> CreatePerson(Person model)
    {
        await model.Save();
        return Ok(model.Id);
    }

    /// <summary>
    /// Update an existing person.
    /// </summary>
    /// <param name="model">Updated data.</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(Person model)
    {
        await model.Save();
        return Ok();
    }

    /// <summary>
    /// Get a list of a person's favorite people.
    /// </summary>
    /// <param name="ownerPersonId">ID of the person.</param>
    /// <returns></returns>
    [HttpGet("{ownerPersonId}/favorite-persons")]
    public async Task<ActionResult<IEnumerable<Person>>> GetFavoritePersons(int ownerPersonId)
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
    public async Task<IActionResult> AddPersonToFavorites(int favoritePersonId, int ownerPersonId)
    {
        await _personService.AddPersonToFavorites(favoritePersonId, ownerPersonId);
        return Ok();
    }

    /// <summary>
    /// Get a list of person's wishlist items.
    /// </summary>
    /// <param name="personId">ID of the person.</param>
    /// <returns>List of items.</returns>
    [HttpGet("{personId}/items")]
    public async Task<ActionResult<IEnumerable<WishlistItem>>> GetPersonWithlist(int personId)
    {
        var result = await _wishlistItemService.GetByPersonId(personId);
        return Ok(result);
    }
}