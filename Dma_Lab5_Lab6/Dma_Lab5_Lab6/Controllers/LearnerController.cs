using Dma_Lab5_Lab6.Data;
using Dma_Lab5_Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Dma_Lab5_Lab6.Controllers
{
    public class LearnerController : Controller
    {
        private readonly SchoolContext db;
        private const int pageSize = 3;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        // GET: Learner/Index
        public IActionResult Index(int? mid, string? keyword, int? pageIndex, bool ajax = true)
        {
            ViewBag.UseAjax = ajax;
            return View(GetLearners(mid, keyword, pageIndex));
        }

        [HttpGet]
        public IActionResult LearnerByMajorID(int? mid)
        {
            return PartialView("LearnerTable", GetLearners(mid, null, 1));
        }

        [HttpGet]
        public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
        {
            return PartialView("LearnerTable", GetLearners(mid, keyword, pageIndex));
        }

        private List<Learner> GetLearners(int? mid, string? keyword, int? pageIndex)
        {
            var learners = db.Learners.AsNoTracking().AsQueryable();
            keyword = keyword?.Trim();
            if (mid.HasValue)
            {
                learners = learners.Where(l => l.MajorID == mid.Value);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                var search = keyword.ToLower();
                learners = learners.Where(l => l.FirstMidName.ToLower().Contains(search));
            }

            int pageNum = (int)Math.Ceiling(learners.Count() / (double)pageSize);
            int page = Math.Clamp(pageIndex ?? 1, 1, Math.Max(1, pageNum));
            ViewBag.mid = mid;
            ViewBag.keyword = keyword;
            ViewBag.pageNum = pageNum;
            ViewBag.pageIndex = page;

            return learners.OrderBy(l => l.LearnerID)
                .Skip(pageSize * (page - 1)).Take(pageSize)
                .Include(l => l.Major).ToList();
        }

        // GET: Learner/Create
        public IActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        // POST: Learner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerID))
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
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        // GET: Learner/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners
                .Include(l => l.Major)
                .Include(e => e.Enrollments)
                .FirstOrDefault(m => m.LearnerID == id);

            if (learner == null)
            {
                return NotFound();
            }

            if (learner.Enrollments != null && learner.Enrollments.Count > 0)
            {
                return Content("Không thể xóa người học đã đăng ký môn học!");
            }

            return View(learner);
        }

        // POST: Learner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.Learners == null)
            {
                return Problem("Entity set 'Learners' is null.");
            }
            if (db.Enrollments.Any(e => e.LearnerID == id))
            {
                return Content("Không thể xóa người học đã đăng ký môn học!");
            }
            var learner = db.Learners.Find(id);
            if (learner != null)
            {
                db.Learners.Remove(learner);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool LearnerExists(int id)
        {
            return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault();
        }
    }
}
