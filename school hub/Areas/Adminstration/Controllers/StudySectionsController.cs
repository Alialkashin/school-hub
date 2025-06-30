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
             
            if (ModelState.IsValid)
            {
                StudySection studySection = new StudySection();
                if (vmstudySection.File != null && vmstudySection.File.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironmentstudysection.WebRootPath, "images/StudySectionsS/");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + vmstudySection.File.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    Directory.CreateDirectory(uploadsFolder);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await vmstudySection.File.CopyToAsync(fileStream);
                    }

                    studySection.ImagePath = "/images/StudySectionsS/" + uniqueFileName;
                }
                studySection.Name = vmstudySection.Name;
                studySection.Description = vmstudySection.Description;
            


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


            var studysection = await _context.Sections.FindAsync(id);
            if (studysection == null)
            {
                return NotFound();
            }

            var viewModel = new InputstudySectionViewModel
            {
                SectionId = studysection.SectionId,
                Name = studysection.Name,
                Description = studysection.Description,
                ExistingImagePath = studysection.ImagePath
            };

            return View(viewModel);
        }

        // POST: Adminstration/StudySections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InputstudySectionViewModel model )
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var studysection = await _context.Sections.OfType<StudySection>()
                    .FirstOrDefaultAsync(s => s.SectionId == model.SectionId);
            if (studysection == null)
            {
                return NotFound();
            }

            try
            {
                if (model.File != null && model.File.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironmentstudysection.WebRootPath, "images/StudySectionsS/");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    Directory.CreateDirectory(uploadsFolder);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    if (!string.IsNullOrEmpty(studysection.ImagePath))
                    {
                        var oldImagePath = Path.Combine(_hostingEnvironmentstudysection.WebRootPath, studysection.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    studysection.ImagePath = "/images/StudySectionsS/" + uniqueFileName;
                }


                studysection.Name = model.Name;
                studysection.Description = model.Description;
              



                _context.Update(studysection);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudySectionExists(studysection.SectionId))
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var section = await _context.Sections.OfType<StudySection>().FirstOrDefaultAsync(s => s.SectionId == id);
            if (section != null)
            {
                if (!string.IsNullOrEmpty(section.ImagePath))
                {
                    var path = Path.Combine(_hostingEnvironmentstudysection.WebRootPath, section.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                _context.Sections.Remove(section);
                await _context.SaveChangesAsync();
                return Content("done");
            }
            return Content("fail");
        }

        private bool StudySectionExists(int id)
        {
            return _context.Sections.Any(e => e.SectionId == id);
        }
    }
}
