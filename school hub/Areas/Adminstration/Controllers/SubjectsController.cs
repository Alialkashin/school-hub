using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    public class SubjectsController : Controller
    {
        private readonly AppDBContext _context;

        public SubjectsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Adminstration/Subjects
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Subjects.Include(s => s.StudyPlan).Include(s => s.Teacher);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Adminstration/Subjects/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .Include(s => s.StudyPlan)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Adminstration/Subjects/Create
        public IActionResult Create()
        {
            ViewData["StudyPlanId"] = new SelectList(_context.StudyPlans, "StudyPlanId", "StudyPlanId");
            ViewData["TeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "Id");
            return View();
        }

        // POST: Adminstration/Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,TotalDuration,TeacherId,StudyPlanId,Name,Description,ImagePath")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudyPlanId"] = new SelectList(_context.StudyPlans, "StudyPlanId", "StudyPlanId", subject.StudyPlanId);
            ViewData["TeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "Id", subject.TeacherId);
            return View(subject);
        }

        // GET: Adminstration/Subjects/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            ViewData["StudyPlanId"] = new SelectList(_context.StudyPlans, "StudyPlanId", "StudyPlanId", subject.StudyPlanId);
            ViewData["TeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "Id", subject.TeacherId);
            return View(subject);
        }

        // POST: Adminstration/Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("SubjectId,TotalDuration,TeacherId,StudyPlanId,Name,Description,ImagePath")] Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.SubjectId))
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
            ViewData["StudyPlanId"] = new SelectList(_context.StudyPlans, "StudyPlanId", "StudyPlanId", subject.StudyPlanId);
            ViewData["TeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "Id", subject.TeacherId);
            return View(subject);
        }

        // GET: Adminstration/Subjects/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .Include(s => s.StudyPlan)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Adminstration/Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(short id)
        {
            return _context.Subjects.Any(e => e.SubjectId == id);
        }
    }
}
