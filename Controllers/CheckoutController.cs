using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ElectroShop.Models;
using ElectroShop.Models.ViewModels;

namespace ElectroShop.Controllers;

public class CheckoutController : Controller
{
    private const string CartSessionKey = "ElectroCart";

    // GET: /checkout
    public IActionResult Index()
    {
        var json = HttpContext.Session.GetString(CartSessionKey);
        var items = json is null
            ? new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new();

        if (!items.Any()) return RedirectToAction("Index", "Cart");

        var vm = new CheckoutViewModel
        {
            Cart = new CartViewModel { Items = items }
        };
        return View(vm);
    }

    // POST: /checkout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PlaceOrder(CheckoutViewModel vm)
    {
        if (!ModelState.IsValid) return View("Index", vm);

        // TODO: lưu đơn hàng vào DB, gửi email xác nhận, v.v.
        HttpContext.Session.Remove(CartSessionKey);

        TempData["OrderSuccess"] = "Đặt hàng thành công! Chúng tôi sẽ liên hệ bạn sớm.";
        return RedirectToAction("Success");
    }

    // GET: /checkout/success
    public IActionResult Success()
    {
        ViewBag.Message = TempData["OrderSuccess"] ?? "Đơn hàng của bạn đã được tiếp nhận.";
        return View();
    }
}
