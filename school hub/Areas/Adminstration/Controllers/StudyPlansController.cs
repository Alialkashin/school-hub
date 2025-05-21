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
    public class StudyPlansController : Controller
    {
        private readonly AppDBContext _context;

        public StudyPlansController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Adminstration/StudyPlans
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.StudyPlans.Include(s => s.StudySection);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Adminstration/StudyPlans/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyPlan = await _context.StudyPlans
                .Include(s => s.StudySection)
                .FirstOrDefaultAsync(m => m.StudyPlanId == id);
            if (studyPlan == null)
            {
                return NotFound();
            }

            return View(studyPlan);
        }

        // GET: Adminstration/StudyPlans/Create
        public IActionResult Create()
        {
            ViewData["StudySectionId"] = new SelectList(_context.Set<StudySection>(), "SectionId", "SectionId");
            return View();
        }

        // POST: Adminstration/StudyPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudyPlanId,StudySectionId,Name,Description,ImagePath")] StudyPlan studyPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studyPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudySectionId"] = new SelectList(_context.Set<StudySection>(), "SectionId", "SectionId", studyPlan.StudySectionId);
            return View(studyPlan);
        }

        // GET: Adminstration/StudyPlans/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyPlan = await _context.StudyPlans.FindAsync(id);
            if (studyPlan == null)
            {
                return NotFound();
            }
            ViewData["StudySectionId"] = new SelectList(_context.Set<StudySection>(), "SectionId", "SectionId", studyPlan.StudySectionId);
            return View(studyPlan);
        }

        // POST: Adminstration/StudyPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("StudyPlanId,StudySectionId,Name,Description,ImagePath")] StudyPlan studyPlan)
        {
            if (id != studyPlan.StudyPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studyPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyPlanExists(studyPlan.StudyPlanId))
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
            ViewData["StudySectionId"] = new SelectList(_context.Set<StudySection>(), "SectionId", "SectionId", studyPlan.StudySectionId);
            return View(studyPlan);
        }

        // GET: Adminstration/StudyPlans/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyPlan = await _context.StudyPlans
                .Include(s => s.StudySection)
                .FirstOrDefaultAsync(m => m.StudyPlanId == id);
            if (studyPlan == null)
            {
                return NotFound();
            }

            return View(studyPlan);
        }

        // POST: Adminstration/StudyPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var studyPlan = await _context.StudyPlans.FindAsync(id);
            if (studyPlan != null)
            {
                _context.StudyPlans.Remove(studyPlan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyPlanExists(short id)
        {
            return _context.StudyPlans.Any(e => e.StudyPlanId == id);
        }
    }
}
