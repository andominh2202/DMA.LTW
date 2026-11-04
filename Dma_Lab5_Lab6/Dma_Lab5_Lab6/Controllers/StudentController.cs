using Dma_Lab5_Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dma_Lab5_Lab6.Controllers
{
    [Route("Admin/Student")]
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>()
        {
            new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "A1-2018", Email = "nam@g.com" },
            new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Address = "A1-2019", Email = "tu@g.com" },
            new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Address = "A1-2020", Email = "phong@g.com" },
            new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "A1-2021", Email = "mai@g.com" }
        };

        [HttpGet("List")]
        [HttpGet("/Student/Index")]
        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet("Add")]
        [HttpGet("/Student/Create")]
        public IActionResult Create()
        {
            PopulateViewBag();
            return View();
        }

        [HttpPost("Add")]
        [HttpPost("/Student/Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                s.Id = (listStudents.Any() ? listStudents.Max(x => x.Id) : 100) + 1;
                listStudents.Add(s);
                return RedirectToAction(nameof(Index));
            }
            PopulateViewBag();
            return View(s);
        }

        private void PopulateViewBag()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "IT", Value = "0" },
                new SelectListItem() { Text = "BE", Value = "1" },
                new SelectListItem() { Text = "CE", Value = "2" },
                new SelectListItem() { Text = "EE", Value = "3" }
            };
        }
    }
}