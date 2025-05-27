using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using school_hub.Areas.Adminstration.ViewModels;
using school_hub.Data;
using school_hub.Models;
using school_hub.ViewModels;
using school_hub.ViewModles;

namespace school_hub.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    public class StudySectionsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _hostingEnvironmentstudysection;
        public StudySectionsController(AppDBContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
           _hostingEnvironmentstudysection = hostingEnvironment;
        }

        // GET: Adminstration/StudySections
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.OfType<StudySection>().ToListAsync());
        }

        // GET: Adminstration/StudySections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studySection = await _context.Sections.OfType<StudySection>()
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
            return View(new InputDisplayInfoViewModel());
        }

        // POST: Adminstration/StudySections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputDisplayInfoViewModel vmstudySection)
        {
             StudySection studySection = new StudySection();
            if (ModelState.IsValid)
            {
                if (vmstudySection.File != null && vmstudySection.File.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironmentstudysection.WebRootPath, "images/StudySections/");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + vmstudySection.File.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    Directory.CreateDirectory(uploadsFolder);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await vmstudySection.File.CopyToAsync(fileStream);
                    }

                    studySection.ImagePath = "/images/StudySections/" + uniqueFileName;
                }
                studySection.Name = vmstudySection.Name;
                studySection.Description = vmstudySection.Description;
                studySection.SectionType = enSectionType.StudySection;
                _context.Sections.Add(studySection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            return View(vmstudySection);
        }

        // GET: Adminstration/StudySections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studySection = await _context.Sections.FindAsync(id);
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

            var studySection = await _context.Sections
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
            var studySection = await _context.Sections.FindAsync(id);
            if (studySection != null)
            {
                _context.Sections.Remove(studySection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudySectionExists(int id)
        {
            return _context.Sections.Any(e => e.SectionId == id);
        }
    }
}
