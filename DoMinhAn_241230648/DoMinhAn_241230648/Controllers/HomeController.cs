using DoMinhAn_241230648.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DoMinhAn_241230648.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLNhaSachContext _db;

        public HomeController(QLNhaSachContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.TheLoai = _db.TheLoais.ToList();
            var dsSach = _db.Saches
                .Where(s => s.Gia >= 100)
                .ToList();
            return View(dsSach);
        }

        [HttpGet]
        public IActionResult LocSach (int id)
        {
            var dsSach = _db.Saches
                .Where(s => s.MaTl == id)
                .ToList();
            return PartialView("~/Views/Home/_DanhSachSach.cshtml", dsSach);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
