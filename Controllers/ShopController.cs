using Microsoft.AspNetCore.Mvc;
using ElectroShop.Models;
using ElectroShop.Models.ViewModels;

namespace ElectroShop.Controllers;

public class ShopController : Controller
{
    private readonly ProductDataService _dataService;

    public ShopController(ProductDataService dataService)
    {
        _dataService = dataService;
    }

    // GET: /shop  hoặc  /shop?query=...&category=...&sortBy=...
    public IActionResult Index(string? query, string? category, string? sortBy,
                               decimal? minPrice, decimal? maxPrice)
    {
        var vm = new ShopViewModel
        {
            Products         = _dataService.Search(query, category, sortBy, minPrice, maxPrice),
            Categories       = _dataService.GetCategories(),
            Query            = query,
            SelectedCategory = category,
            SortBy           = sortBy,
            MinPrice         = minPrice,
            MaxPrice         = maxPrice,
        };
        return View(vm);
    }

    // GET: /shop/product/{id}
    public IActionResult Detail(int id)
    {
        var product = _dataService.GetById(id);
        if (product is null) return NotFound();

        var related = _dataService.Search(null, product.Category, null, null, null)
                                  .Where(p => p.Id != id)
                                  .Take(8)
                                  .ToList();

        var vm = new ProductDetailViewModel
        {
            Product        = product,
            RelatedProducts = related,
        };
        return View(vm);
    }
}
// GET: /shop/bestseller
public IActionResult Bestseller()
{
    var vm = new ShopViewModel
    {
        Products   = _dataService.GetBestSellers(),
        Categories = _dataService.GetCategories(),
    };
    ViewData["Title"] = "Best Sellers";
    return View("Index", vm); // dùng lại View Index của Shop
}