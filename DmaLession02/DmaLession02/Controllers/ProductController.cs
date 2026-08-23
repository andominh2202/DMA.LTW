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
                new Product { Id = 1, Name = "Laptop Lenovo LOQ", Price = 19490000, CreatedAt = DateTime.Now.AddDays(-10), Image = "LaptopLenovoLOQ.jpg"},
                new Product { Id = 2, Name = "Bàn phím cơ Aula F75", Price = 650000, CreatedAt = DateTime.Now.AddDays(-5), Image = "BanPhimCoAulaF75.jpg" },
                new Product { Id = 3, Name = "Chuột không dây Razer", Price = 950000, CreatedAt = DateTime.Now.AddDays(-3), Image = "ChuotLogitech.jpg" },
                new Product { Id = 4, Name = "Màn hình Xiaomi", Price = 6200000, CreatedAt = DateTime.Now.AddDays(-1), Image = "ManHinhXiaomi.jpg" }
            };

            // Truyền danh sách sang View
            return View(products);
        }
    }
}