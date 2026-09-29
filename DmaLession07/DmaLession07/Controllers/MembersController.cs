using DmaLession07.Models.DataModels;
using DmaLession07.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace DmaLession07.Controllers
{
    public class MembersController : Controller
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

        // Action POST 1: Nhận Member và validate thủ công
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

            string patternEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
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

        [HttpPost]
        [ActionName("CreateWithViewModel")]
        public IActionResult Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var member = new Member
                {
                    MemberID = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    FullName = model.FullName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Birthday = model.Birthday
                };
                members.Add(member);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}