using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NuGet.Packaging;
using school_hub.Areas.Adminstration.ViewModels;
using school_hub.ViewModels;
using school_hub.Data;
using school_hub.Models;
using static System.Net.Mime.MediaTypeNames;

namespace school_hub.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    public class LibrarySectionsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _hostingEnvironmentlibary;
        public LibrarySectionsController(AppDBContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironmentlibary = hostingEnvironment;
        }

        // GET: Adminstration/LibrarySections
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.OfType<LibrarySection>().ToListAsync());
        }

        // GET: Adminstration/LibrarySections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarySection = await _context.Sections
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
            InputLibrarySectionViewModel model = new InputLibrarySectionViewModel();
            model.Books = new List<InputBookViewModel>();
            return View(model);
        }



        


    


        // POST: Adminstration/LibrarySections/Create
        // To protect from overpostin attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputDisplayInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                LibrarySection librarySection = new LibrarySection();
                if (model.File != null && model.File.Length > 0)
                {

                    var uploadsFolder = Path.Combine(_hostingEnvironmentlibary.WebRootPath, "images/library/");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    Directory.CreateDirectory(uploadsFolder);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    librarySection.ImagePath = "/images/library/" + uniqueFileName;
                }
                librarySection.Name = model.Name;
                librarySection.Description = model.Description;
                



                _context.Sections.Add(librarySection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            return View(model);
        }


        // GET: Adminstration/LibrarySections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarySection = await _context.Sections.FindAsync(id);
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

            var librarySection = await _context.Sections
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
            var librarySection = await _context.Sections.FindAsync(id);
            if (librarySection != null)
            {
                _context.Sections.Remove(librarySection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibrarySectionExists(int id)
        {
            return _context.Sections.Any(e => e.SectionId == id);
        }
    }
}
