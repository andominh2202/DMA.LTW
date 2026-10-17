using Microsoft.AspNetCore.Mvc;

namespace DmaLession09.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Trang quản trị";
            return View();
        }
    }
}
