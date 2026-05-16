// Controllers/AboutController.cs
using Microsoft.AspNetCore.Mvc;

namespace ElectroShop.Controllers;

public class AboutController : Controller
{
    public IActionResult Index() => View();
}
