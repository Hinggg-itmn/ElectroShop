using Microsoft.AspNetCore.Mvc;
using ElectroShop.Models.ViewModels;

namespace ElectroShop.Controllers;

public class AccountController : Controller
{
    // GET: /account/login
    public IActionResult Login() => View(new LoginViewModel());

    // POST: /account/login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel vm)
    {
        // Demo: admin@electro.com / 123456
        // TODO: thay bằng kiểm tra database thật + ASP.NET Identity
        if (vm.Email == "admin@electro.com" && vm.Password == "123456")
        {
            HttpContext.Session.SetString("UserEmail", vm.Email);
            TempData["LoginSuccess"] = "Đăng nhập thành công!";
            return RedirectToAction("Index", "Home");
        }

        vm.ErrorMessage = "Email hoặc mật khẩu không đúng. Thử: admin@electro.com / 123456";
        return View(vm);
    }

    // GET: /account/register
    public IActionResult Register() => View(new RegisterViewModel());

    // POST: /account/register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        // TODO: lưu user vào database
        TempData["RegisterSuccess"] = $"Đăng ký thành công! Chào mừng {vm.FirstName} {vm.LastName}!";
        return RedirectToAction(nameof(Login));
    }

    // GET: /account/logout
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UserEmail");
        TempData["LogoutSuccess"] = "Đã đăng xuất thành công.";
        return RedirectToAction("Index", "Home");
    }
}
