using DmaLession08.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DmaLession08.Controllers
{
    public class BookController : Controller
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index(string? searchString, int? categoryId, int? publisherId, int page = 1)
        {
            const int pageSize = 8;
            var query = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var term = searchString.Trim().ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(term) || (b.Author != null && b.Author.ToLower().Contains(term)));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(b => b.CategoryId == categoryId.Value);
            }

            if (publisherId.HasValue && publisherId.Value > 0)
            {
                query = query.Where(b => b.PublisherId == publisherId.Value);
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages > 0 ? totalPages : 1));

            var books = await query
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.Categories = new SelectList(await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync(), "CategoryId", "CategoryName", categoryId);
            ViewBag.Publishers = new SelectList(await _context.Publishers.OrderBy(p => p.PublisherName).ToListAsync(), "PublisherId", "PublisherName", publisherId);
            ViewBag.SearchString = searchString;
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SelectedPublisherId = publisherId;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalCount = totalCount;

            return View(books);
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            // Gợi ý các sách cùng thể loại
            ViewBag.RelatedBooks = await _context.Books
                .Where(b => b.CategoryId == book.CategoryId && b.BookId != book.BookId)
                .Take(4)
                .ToListAsync();

            return View(book);
        }

        // GET: Book/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.PublisherId = new SelectList(await _context.Publishers.OrderBy(p => p.PublisherName).ToListAsync(), "PublisherId", "PublisherName");
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Author,Release,Price,Description,Picture,PublisherId,CategoryId")] Book book)
        {
            if (await _context.Books.AnyAsync(b => b.BookId == book.BookId))
            {
                ModelState.AddModelError("BookId", "Mã sách này đã tồn tại trong hệ thống!");
            }

            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Đã thêm mới sách \"{book.Title}\" thành công!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync(), "CategoryId", "CategoryName", book.CategoryId);
            ViewBag.PublisherId = new SelectList(await _context.Publishers.OrderBy(p => p.PublisherName).ToListAsync(), "PublisherId", "PublisherName", book.PublisherId);
            return View(book);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync(), "CategoryId", "CategoryName", book.CategoryId);
            ViewBag.PublisherId = new SelectList(await _context.Publishers.OrderBy(p => p.PublisherName).ToListAsync(), "PublisherId", "PublisherName", book.PublisherId);
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BookId,Title,Author,Release,Price,Description,Picture,PublisherId,CategoryId")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Cập nhật thông tin sách \"{book.Title}\" thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Books.AnyAsync(e => e.BookId == book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync(), "CategoryId", "CategoryName", book.CategoryId);
            ViewBag.PublisherId = new SelectList(await _context.Publishers.OrderBy(p => p.PublisherName).ToListAsync(), "PublisherId", "PublisherName", book.PublisherId);
            return View(book);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                // Kiểm tra ràng buộc khoá ngoại nếu có OrderDetail
                var hasOrders = await _context.OrderDetails.AnyAsync(od => od.BookId == id);
                if (hasOrders)
                {
                    TempData["Error"] = $"Không thể xóa sách \"{book.Title}\" vì đã có đơn đặt hàng liên kết!";
                    return RedirectToAction(nameof(Index));
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Đã xóa sách \"{book.Title}\" thành công!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
