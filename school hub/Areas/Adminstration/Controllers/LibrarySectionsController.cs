using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using school_hub.Areas.Adminstration.ViewModels;
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

            return View();

        }



        


    //    public List<SelectListItem> GetSectionTypeSelectList()
    //{
    //    var enumType = typeof(enSectionType);
    //    var values = Enum.GetValues(enumType).Cast<enSectionType>();

    //    var items = values.Select(e => new SelectListItem
    //    {
    //        Value = ((int)e).ToString(),
    //        Text = e.GetDisplayName() // تستخدم امتدادك الخاص هنا
    //    }).ToList();

    //    return items;
    //}


        // POST: Adminstration/LibrarySections/Create
        // To protect from overpostin attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputLibraryViewModel model)
        {
            if (ModelState.IsValid)
            {
               LibrarySection library=new LibrarySection();
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

                    library.ImagePath = "/images/library/" + uniqueFileName;
                }
                library.Name = model.Name;
                library.Description = model.Description;




                _context.Add(library);
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

            var viewModel = new InputLibraryViewModel
            {
                SectionId = librarySection.SectionId,
                Name = librarySection.Name,
                Description = librarySection.Description,
                ExistingImagePath = librarySection.ImagePath 
            };

            return View(viewModel);
     
        }

        // POST: Adminstration/LibrarySections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InputLibraryViewModel model)
        {
            if (!ModelState.IsValid)
            { 
                return View(model);
            }

            var library = await _context.Sections.OfType<LibrarySection>()
                    .FirstOrDefaultAsync(s => s.SectionId == model.SectionId);
            if (library == null)
            {
                return NotFound();
            }

            try
            {
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

                    if (!string.IsNullOrEmpty(library.ImagePath))
                    {
                        var oldImagePath = Path.Combine(_hostingEnvironmentlibary.WebRootPath, library.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                   library.ImagePath = "/images/library/" + uniqueFileName;
                }


                library.Name = model.Name;
                library.Description = model.Description;
               

                _context.Update(library);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibrarySectionExists(library.SectionId))
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
