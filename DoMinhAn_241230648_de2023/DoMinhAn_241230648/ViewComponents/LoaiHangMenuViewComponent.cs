using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DoMinhAn_241230648_de2023.Models;

namespace DoMinhAn_241230648_de2023.ViewComponents
{
    public class LoaiHangMenuViewComponent : ViewComponent
    {
        private readonly QLHangHoaContext _db;

        public LoaiHangMenuViewComponent(QLHangHoaContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult>
            InvokeAsync()
        {
            var DanhSach = await _db.LoaiHangs.AsNoTracking().ToListAsync();
            return View(DanhSach);
        }
    }
}
