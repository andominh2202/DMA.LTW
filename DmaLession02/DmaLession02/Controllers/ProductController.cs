using Microsoft.AspNetCore.Mvc;
using DmaLesson02.Models;

namespace DmaLesson02.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            // Tạo danh sách 4 sản phẩm mẫu
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop Gaming Asus", Price = 25000000, CreatedAt = DateTime.Now.AddDays(-10), Image = "laptop.jpg" },
                new Product { Id = 2, Name = "Bàn phím cơ Logitech", Price = 1800000, CreatedAt = DateTime.Now.AddDays(-5), Image = "keyboard.jpg" },
                new Product { Id = 3, Name = "Chuột không dây Razer", Price = 950000, CreatedAt = DateTime.Now.AddDays(-3), Image = "mouse.jpg" },
                new Product { Id = 4, Name = "Màn hình UltraWide LG", Price = 6200000, CreatedAt = DateTime.Now.AddDays(-1), Image = "monitor.jpg" }
            };

            // Truyền danh sách sang View
            return View(products);
        }
    }
}