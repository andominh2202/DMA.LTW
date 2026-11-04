using Dma_Lab5_Lab6.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dma_Lab5_Lab6.ViewComponents
{
    public class MajorViewComponent : ViewComponent
    {
        private readonly SchoolContext db;

        public MajorViewComponent(SchoolContext context)
        {
            db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var majors = await db.Majors.AsNoTracking().OrderBy(m => m.MajorID).ToListAsync();
            return View("RenderMajor", majors);
        }
    }
}
