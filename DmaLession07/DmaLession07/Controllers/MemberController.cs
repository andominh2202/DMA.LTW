using DmaLession07.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace DmaLession07.Controllers
{
    public class MemberController : Controller
    {
        public static readonly List<Member> members = new List<Member>();
        public IActionResult Index()
        {
            return View(members);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Member member)
        {
            string msg = null;
            bool validate = true;
            if (string.IsNullOrEmpty(member.UserName) || member.UserName.Length < 3 || member.UserName.Length > 20)
            {
                msg = "<li>Tên đăng nhập phải từ 3 đến 20 ký tự</li>";
                validate = false;
            }
            string patternEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"; ;
            if (string.IsNullOrWhiteSpace(member.Email) || !Regex.IsMatch(member.Email, patternEmail))
            {
                msg += "<li>Email không hợp lệ</li>";
                validate = false;
            }
            if (validate)
            {
                member.MemberID = Guid.NewGuid().ToString();
                members.Add(member);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Msg = "<div class='alert alert-danger'>" + msg + "</div>";
                return View(member);
            }
        }
    }
}
