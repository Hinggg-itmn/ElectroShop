using Microsoft.AspNetCore.Mvc;
using ElectroShop.Models;
using ElectroShop.Models.ViewModels;

namespace ElectroShop.Controllers;

public class HomeController : Controller
{
    private readonly ProductDataService _dataService;

    public HomeController(ProductDataService dataService)
    {
        _dataService = dataService;
    }

    // GET: /
    public IActionResult Index()
    {
        var vm = new HomeViewModel
        {
            FeaturedProducts = _dataService.GetFeatured(),
            BestSellers      = _dataService.GetBestSellers(),
            NewArrivals      = _dataService.GetNewArrivals(),
        };
        return View(vm);
    }
    public IActionResult TestHeader()
    {
    return View();
    }
}
