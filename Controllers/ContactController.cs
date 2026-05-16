using Microsoft.AspNetCore.Mvc;
using ElectroShop.Models.ViewModels;

namespace ElectroShop.Controllers;

public class ContactController : Controller
{
    // GET: /contact
    public IActionResult Index() => View(new ContactViewModel());

    // POST: /contact
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ContactViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        // TODO: gửi email, lưu DB, v.v.
        vm.IsSubmitted = true;
        ModelState.Clear();
        return View(new ContactViewModel { IsSubmitted = true });
    }
}
