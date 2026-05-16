using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ElectroShop.Models;
using ElectroShop.Models.ViewModels;

namespace ElectroShop.Controllers;

public class CartController : Controller
{
    private const string CartSessionKey = "ElectroCart";
    private readonly ProductDataService _dataService;

    public CartController(ProductDataService dataService)
    {
        _dataService = dataService;
    }

    // ── Helpers ───────────────────────────────────────────────────────────

    private List<CartItem> GetCart()
    {
        var json = HttpContext.Session.GetString(CartSessionKey);
        return json is null
            ? new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new();
    }

    private void SaveCart(List<CartItem> cart)
    {
        HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
    }

    // ── Actions ───────────────────────────────────────────────────────────

    // GET: /cart
    public IActionResult Index()
    {
        var vm = new CartViewModel { Items = GetCart() };
        return View(vm);
    }

    // POST: /cart/add
    [HttpPost]
    public IActionResult Add(int productId, int quantity = 1)
    {
        var product = _dataService.GetById(productId);
        if (product is null) return NotFound();

        var cart = GetCart();
        var existing = cart.FirstOrDefault(i => i.ProductId == productId);

        if (existing is not null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                Name      = product.Name,
                ImageUrl  = product.ImageUrl,
                UnitPrice = product.Price,
                Quantity  = quantity,
            });
        }

        SaveCart(cart);

        // AJAX request → trả JSON
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return Json(new { success = true, count = cart.Sum(i => i.Quantity) });

        return RedirectToAction(nameof(Index));
    }

    // POST: /cart/update
    [HttpPost]
    public IActionResult Update(int productId, int quantity)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            if (quantity <= 0) cart.Remove(item);
            else item.Quantity = quantity;
        }
        SaveCart(cart);
        return RedirectToAction(nameof(Index));
    }

    // POST: /cart/remove
    [HttpPost]
    public IActionResult Remove(int productId)
    {
        var cart = GetCart();
        cart.RemoveAll(i => i.ProductId == productId);
        SaveCart(cart);
        return RedirectToAction(nameof(Index));
    }

    // GET: /cart/count  (dùng cho AJAX cập nhật badge)
    [HttpGet]
    public IActionResult Count()
    {
        var count = GetCart().Sum(i => i.Quantity);
        return Json(count);
    }
}
