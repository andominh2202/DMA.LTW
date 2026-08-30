using DmaLession03.Models;
using Microsoft.AspNetCore.Mvc;

namespace DmaLession03.Controllers
{
    public class ProductController : Controller
    {
        private List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "Quần áo" },
                new Category { Id = 2, Name = "Túi xách" },
                new Category { Id = 3, Name = "Đồng hồ" },
                new Category { Id = 4, Name = "Tivi" },
                new Category { Id = 5, Name = "Tủ lạnh" },
                new Category { Id = 6, Name = "Máy bơm" },
                new Category { Id = 7, Name = "Quạt điện" },
                new Category { Id = 8, Name = "Lò sưởi" }
            };
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Bộ đồ bơi cho trẻ em nam",
                    Image = "/images/doboinam.jpg",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Bộ đồ bơi cho trẻ em nam chất lượng cao.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },

                new Product
                {
                    Id = 2,
                    Name = "Bộ đồ bơi cho trẻ em nữ",
                    Image = "/images/doboinu.jpg",
                    Price = 60000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Bộ đồ bơi cho trẻ em nữ.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },

                new Product
                {
                    Id = 3,
                    Name = "Bộ đồ bơi cho trẻ 3-5 tuổi",
                    Image = "/images/doboitretu3toi5.jpg",
                    Price = 60000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Bộ đồ bơi cho trẻ từ 3 đến 5 tuổi.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },

                new Product
                {
                    Id = 4,
                    Name = "Túi thời trang màu mù 2021",
                    Image = "/images/tuinau.jpg",
                    Price = 60000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Túi thời trang nữ.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },

                new Product
                {
                    Id = 5,
                    Name = "Túi thời trang da cá sấu",
                    Image = "/images/tuidacasau.jpg",
                    Price = 60000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Túi thời trang da cá sấu.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },

                new Product
                {
                    Id = 6,
                    Name = "Túi thời trang nữ",
                    Image = "/images/tuithoitrangnu.jpg",
                    Price = 60000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Túi thời trang nữ cao cấp.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                }
            };
        }

        public IActionResult Index()
        {
            ViewBag.Products = GetProducts();
            ViewBag.Categories = GetCategories();

            return View();
        }

        public IActionResult Category(int categoryId)
        {
            var products = GetProducts()
                .Where(x => x.CategoryId == categoryId)
                .ToList();

            ViewBag.Products = products;
            ViewBag.Categories = GetCategories();

            return View("Index");
        }

        public IActionResult Detail(int id)
        {
            var product = GetProducts()
                .FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}