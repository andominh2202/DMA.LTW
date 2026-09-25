using DmaLession06.Models.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace DmaLession06.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            var member = new Member();
            member.MemberId = Guid.NewGuid().ToString();
            member.UserName = "andominh2022";
            member.FullName = "Do Minh An";
            member.Password = "";
            member.Email = "dominhan0612@gmail.com";

            return View(member);
        }

        public static readonly List<Member> members = new List<Member>()
        {
            new Member { MemberId = Guid.NewGuid().ToString(), UserName = "andominh2022", FullName = "Do Minh An", Password = "", Email = "dominhan0612@gmail.com"},
            new Member { MemberId = Guid.NewGuid().ToString(), UserName = "member1", FullName = "Thành viên 1", Password = "1234356", Email = "tv1@gmail.com" },
            new Member { MemberId = Guid.NewGuid().ToString(), UserName = "member2", FullName = "Thành viên 2", Password = "1234356", Email = "tv2@gmail.com" },
            new Member { MemberId = Guid.NewGuid().ToString(), UserName = "member3", FullName = "Thành viên 3", Password = "1234356", Email = "tv3@gmail.com" }
        };
        public IActionResult GetMembers()
        {
            ViewBag.members = members;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Member member)
        {
            member.MemberId = Guid.NewGuid().ToString();
            members.Add(member);
            return RedirectToAction("GetMembers");
        }
    }
   }
