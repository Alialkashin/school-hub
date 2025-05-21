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
    public class StudySectionsController : Controller
    {
        private readonly AppDBContext _context;

        public StudySectionsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Adminstration/StudySections
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudySection.ToListAsync());
        }

        // GET: Adminstration/StudySections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studySection = await _context.StudySection
                .FirstOrDefaultAsync(m => m.SectionId == id);
            if (studySection == null)
            {
                return NotFound();
            }

            return View(studySection);
        }

        // GET: Adminstration/StudySections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adminstration/StudySections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectionId,SectionType,Name,Description,ImagePath")] StudySection studySection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studySection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studySection);
        }

        // GET: Adminstration/StudySections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studySection = await _context.StudySection.FindAsync(id);
            if (studySection == null)
            {
                return NotFound();
            }
            return View(studySection);
        }

        // POST: Adminstration/StudySections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectionId,SectionType,Name,Description,ImagePath")] StudySection studySection)
        {
            if (id != studySection.SectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studySection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudySectionExists(studySection.SectionId))
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
            return View(studySection);
        }

        // GET: Adminstration/StudySections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studySection = await _context.StudySection
                .FirstOrDefaultAsync(m => m.SectionId == id);
            if (studySection == null)
            {
                return NotFound();
            }

            return View(studySection);
        }

        // POST: Adminstration/StudySections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studySection = await _context.StudySection.FindAsync(id);
            if (studySection != null)
            {
                _context.StudySection.Remove(studySection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudySectionExists(int id)
        {
            return _context.StudySection.Any(e => e.SectionId == id);
        }
    }
}
