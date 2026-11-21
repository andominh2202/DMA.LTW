using DoMinhAn_241230648_de2026_QLTS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DoMinhAn_241230648_de2026_QLTS.Controllers
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
            var danhSach = await _db.TaiSans.Where(x => x.NguyenGia >= 20).OrderBy(x => x.MaTs).ToListAsync();

            ViewBag.TieuDe = "Đỗ Minh An";
            return View(danhSach);
        }

        public async Task<IActionResult> LocTaiSan(int maLoai)
        {
            var loai = await _db.LoaiTaiSans.FindAsync(maLoai);
            if (loai == null) return NotFound();

            var danhSach = await _db.TaiSans.Where(x => x.MaLoai == maLoai).OrderBy(x => x.MaTs).ToListAsync();
            ViewBag.TieuDe = loai.TenLoai;
            return PartialView("~/Views/Shared/DoMinhAn_MainContent.cshtml", danhSach);
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
