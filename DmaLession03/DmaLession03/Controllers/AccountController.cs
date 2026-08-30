using DmaLession03.Models;
using Microsoft.AspNetCore.Mvc;

namespace DmaLession03.Controllers
{
    public class AccountController : Controller
    {
        private List<Account> GetAccounts()
        {
            return new List<Account>
            {
                new Account
                {
                    Id = 1,
                    Name = "Đỗ Minh An",
                    Email = "dominhan0612@gmail.com",
                    Phone = "0333455447",
                    Avatar = "/images/An.jpg",
                    Address = "Ha Noi",
                    Bio = "Tôi là An đang học lập trình web ",
                    Gender = 1,
                    Birthday = new DateTime(2006, 12, 06)
                }
            };
        }
        public IActionResult Index()
        {
            var accounts = GetAccounts();

            ViewBag.Accounts = accounts;

            return View();
        }

        public IActionResult Profile(int id)
        {
            var account = GetAccounts()
                .FirstOrDefault(x => x.Id == id);

            if (account == null)
            {
                return NotFound();
            }

            ViewBag.Account = account;

            return View();
        }
    }
}