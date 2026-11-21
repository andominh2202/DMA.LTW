using DoMinhAn_241230648_de2023.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DoMinhAn_241230648_de2023.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLHangHoaContext _db;

        public HomeController(QLHangHoaContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var danhSach = await _db.HangHoas.Where(x => x.Gia >= 100).AsNoTracking().ToArrayAsync();
            return View(danhSach);
        }

        [HttpGet]

        public IActionResult LocHang(int maLoai)
        {
            var ds = _db.HangHoas.Where(x => x.MaLoai == maLoai).ToList();
            return PartialView("_DanhSachHang", ds);
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
