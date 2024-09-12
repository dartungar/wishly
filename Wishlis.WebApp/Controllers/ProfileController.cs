using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wishlis.WebApp.Controllers;

public class Profile2Controller : Controller
{
    [Authorize]
    [HttpGet("/profile2/test")]
    public IActionResult Index()
    {
        return View();
    }
}