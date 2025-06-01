using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using school_hub.Areas.Adminstration.ViewModels;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Adminstration.Controllers
{

    [Area("Adminstration")]
    public class BooksController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public BooksController(AppDBContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        /*

        // GET: Adminstration/Books
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Books.Include(b => b.LibrarySection);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Adminstration/Books/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.LibrarySection)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Adminstration/Books/Create
        public IActionResult Create()
        {


            InputBookViewModel input = new InputBookViewModel();
            // جلب الأقسام التي نوعها LibrarySection فقط
            var librarySections = _context.Sections
      .OfType<LibrarySection>()
      .Select(s => new { s.SectionId, s.Name })
      .ToList();

            ViewBag.LibrarySections = new SelectList(librarySections, "SectionId", "Name");


            return View();
        }


        // POST: Adminstration/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book();

               
                if (model.File != null && model.File.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/books/");

                    // تأكد من وجود مجلد الرفع
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(stream);
                    }

                    book.BookPath = "/images/books/" + uniqueFileName;
                }

                book.Title = model.Name;
                book.Description = model.Description;

                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // إذا فشل التحقق: إعادة تعبئة القائمة
            var librarySections = _context.Sections
                .OfType<LibrarySection>()
                .Select(s => new { s.SectionId, s.Name })
                .ToList();

            ViewBag.LibrarySections = new SelectList(librarySections, "SectionId", "Name");

            return View(model);
        }

        // GET: Adminstration/Books/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["LibrarySectionId"] = new SelectList(_context.Set<LibrarySection>(), "SectionId", "SectionId", book.LibrarySectionId);
            return View(book);
        }

        // POST: Adminstration/Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book, IFormFile imagefile)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imagefile != null && imagefile.Length > 0)
                    {
                        var uploadsfolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/ccc");
                        var uniquefilename = Guid.NewGuid().ToString() + "_" + imagefile.FileName;
                        var filepath = Path.Combine(uploadsfolder, uniquefilename);
                        using (var filestream = new FileStream(filepath, FileMode.Create))
                        {
                            await imagefile.CopyToAsync(filestream);
                        }
                        if (!string.IsNullOrEmpty(book.BookPath))
                        {
                            var oldimagepath = Path.Combine(_hostingEnvironment.WebRootPath, book.Description.TrimStart('/'));
                            if (System.IO.File.Exists(oldimagepath))
                            {
                                System.IO.File.Delete(oldimagepath);
                            }
                        }
                        book.BookPath = "/images/ccc/" + uniquefilename;
                    }
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            return View(book);
        }

        // GET: Adminstration/Books/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.LibrarySection)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Adminstration/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(short id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
        */
  
    }
}
