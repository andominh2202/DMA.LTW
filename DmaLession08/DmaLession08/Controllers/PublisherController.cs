using DmaLession08.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DmaLession08.Controllers
{
    public class PublisherController : Controller
    {
        private readonly BookStoreDbContext _context;

        public PublisherController(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var publishers = await _context.Publishers
                .Include(p => p.Books)
                .OrderBy(p => p.PublisherName)
                .ToListAsync();
            return View(publishers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublisherId,PublisherName,Phone,Address")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Thêm nhà xuất bản \"{publisher.PublisherName}\" thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null) return NotFound();
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherId,PublisherName,Phone,Address")] Publisher publisher)
        {
            if (id != publisher.PublisherId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publisher);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Cập nhật nhà xuất bản \"{publisher.PublisherName}\" thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Publishers.AnyAsync(e => e.PublisherId == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var publisher = await _context.Publishers
                .Include(p => p.Books)
                .FirstOrDefaultAsync(m => m.PublisherId == id);
            if (publisher == null) return NotFound();
            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Publishers
                .Include(p => p.Books)
                .FirstOrDefaultAsync(p => p.PublisherId == id);

            if (publisher != null)
            {
                if (publisher.Books.Any())
                {
                    TempData["Error"] = $"Không thể xóa NXB \"{publisher.PublisherName}\" vì vẫn còn {publisher.Books.Count} cuốn sách liên kết!";
                    return RedirectToAction(nameof(Index));
                }

                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Xóa nhà xuất bản \"{publisher.PublisherName}\" thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
