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
    public class LibrarySectionsController : Controller
    {
        private readonly AppDBContext _context;

        public LibrarySectionsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Adminstration/LibrarySections
        public async Task<IActionResult> Index()
        {
            return View(await _context.LibrarySection.ToListAsync());
        }

        // GET: Adminstration/LibrarySections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarySection = await _context.LibrarySection
                .FirstOrDefaultAsync(m => m.SectionId == id);
            if (librarySection == null)
            {
                return NotFound();
            }

            return View(librarySection);
        }

        // GET: Adminstration/LibrarySections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adminstration/LibrarySections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectionId,SectionType,Name,Description,ImagePath")] LibrarySection librarySection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(librarySection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(librarySection);
        }

        // GET: Adminstration/LibrarySections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarySection = await _context.LibrarySection.FindAsync(id);
            if (librarySection == null)
            {
                return NotFound();
            }
            return View(librarySection);
        }

        // POST: Adminstration/LibrarySections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectionId,SectionType,Name,Description,ImagePath")] LibrarySection librarySection)
        {
            if (id != librarySection.SectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(librarySection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibrarySectionExists(librarySection.SectionId))
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
            return View(librarySection);
        }

        // GET: Adminstration/LibrarySections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarySection = await _context.LibrarySection
                .FirstOrDefaultAsync(m => m.SectionId == id);
            if (librarySection == null)
            {
                return NotFound();
            }

            return View(librarySection);
        }

        // POST: Adminstration/LibrarySections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var librarySection = await _context.LibrarySection.FindAsync(id);
            if (librarySection != null)
            {
                _context.LibrarySection.Remove(librarySection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibrarySectionExists(int id)
        {
            return _context.LibrarySection.Any(e => e.SectionId == id);
        }
    }
}
