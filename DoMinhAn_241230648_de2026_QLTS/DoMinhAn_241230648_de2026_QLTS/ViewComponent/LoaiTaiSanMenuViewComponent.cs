using DoMinhAn_241230648_de2026_QLTS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoMinhAn_241230648_de2026_QLTS.ViewComponents
{
    public class LoaiTaiSanMenuViewComponent : ViewComponent
    {
        private readonly QLHangHoaContext _db;

        public LoaiTaiSanMenuViewComponent(QLHangHoaContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult>
            InvokeAsync()
        {
            var DanhSach = await _db.LoaiTaiSans.AsNoTracking().ToListAsync();
            return View(DanhSach);
        }
    }
}
