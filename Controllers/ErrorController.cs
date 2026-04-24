using Microsoft.AspNetCore.Mvc;

namespace ElectroShop.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult Index(int statusCode)
    {
        ViewBag.StatusCode = statusCode;
        ViewBag.Message = statusCode switch
        {
            404 => "Trang bạn tìm kiếm không tồn tại.",
            500 => "Máy chủ gặp lỗi. Vui lòng thử lại sau.",
            _   => "Đã xảy ra lỗi không xác định."
        };
        return View("Index");
    }
}
