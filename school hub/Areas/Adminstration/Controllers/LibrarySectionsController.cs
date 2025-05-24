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



            var model = new InputBookViewModel();

            // جلب الأقسام من النوع LibrarySection فقط
            var librarySections = _context.Sections
                .OfType<LibrarySection>()
                .Select(s => new SelectListItem
                {
                    Value = s.SectionId.ToString(),
                    Text = s.Name
                }).ToList();

            model.LibrarySection = librarySections;

            return View(model);





        }



        


        public List<SelectListItem> GetSectionTypeSelectList()
    {
        var enumType = typeof(enSectionType);
        var values = Enum.GetValues(enumType).Cast<enSectionType>();

        var items = values.Select(e => new SelectListItem
        {
            Value = ((int)e).ToString(),
            Text = e.GetDisplayName() // تستخدم امتدادك الخاص هنا
        }).ToList();

        return items;
    }


        // POST: Adminstration/LibrarySections/Create
        // To protect from overpostin attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputBookViewModel model)
        {
            if (ModelState.IsValid)
            {
               Book book=new Book();
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

                    book.BookPath = "/images/library/" + uniqueFileName;
                }
                book.Title = model.Name;
                book.Description = model.Description;
                book.LibrarySectionId = model.LibrarySectionId;



                _context.Books.Add(book);
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
