using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PdfLibCore;
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

            InputBookViewModel model = new InputBookViewModel();
            model.LibrarySectionItems = _context.Set<LibrarySection>()
                  .Select(s => new SelectListItem
                  {
                      Value = s.SectionId.ToString(),
                      Text = s.Name
                  })
             .ToList();
            return View(model);

        }
      


        // POST: Adminstration/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.LibrarySectionItems = _context.Set<LibrarySection>()
                    .Select(s => new SelectListItem
                    {
                        Value = s.SectionId.ToString(),
                        Text = s.Name
                    }).ToList();
                return View(model);
            }

            if (model.File == null || model.File.Length == 0)
            {
                ModelState.AddModelError("File", "يرجى اختيار ملف PDF.");
                return View(model);
            }

            if (!model.File.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("File", "يجب رفع ملف PDF فقط.");
                return View(model);
            }

            // حفظ ملف PDF
            string pdfFolder = Path.Combine(_hostingEnvironment.WebRootPath, "PDF");
            Directory.CreateDirectory(pdfFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
            string pdfFilePath = Path.Combine(pdfFolder, uniqueFileName);

            using (var fileStream = new FileStream(pdfFilePath, FileMode.Create))
            {
                await model.File.CopyToAsync(fileStream);
            }

            // استخراج عدد صفحات باستخدام PdfLibCore
           
            using (var document = new PdfDocument(System.IO.File.ReadAllBytes(pdfFilePath)))
            {
          model.PageCount = document.Pages.Count;
            }

            // حفظ بيانات الكتاب في قاعدة البيانات
            var book = new Book
            {
                Title = model.Name,
                Description = model.Description,
                LibrarySectionId = model.LibrarySectionId,
                BookPath = "/PDF/" + uniqueFileName,
                PageCount = model.PageCount,
                UploadDate = DateTime.Now
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }







        // GET: Adminstration/Books/Edit/5
        // GET: Adminstration/Books/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
                return NotFound();

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            var model = new InputBookViewModel
            {
                Id = book.BookId,
                Name = book.Title,
                Description = book.Description,
                LibrarySectionId = book.LibrarySectionId,
                ExistingImagePath = book.BookPath, // هنا مسار ملف الـ PDF أو الصورة
                LibrarySectionItems = _context.Set<LibrarySection>()
                    .Select(s => new SelectListItem
                    {
                        Value = s.SectionId.ToString(),
                        Text = s.Name
                    }).ToList()
            };

            return View(model);
        }

        // POST: Adminstration/Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, InputBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // إعادة تحميل قائمة الأقسام إذا فشل التحقق
                model.LibrarySectionItems = _context.Set<LibrarySection>()
                    .Select(s => new SelectListItem
                    {
                        Value = s.SectionId.ToString(),
                        Text = s.Name
                    }).ToList();

                return View(model);
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            try
            {
                if (model.File != null && model.File.Length > 0)
                {
                    // تحقق نوع الملف PDF فقط
                    if (!model.File.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError("File", "يجب رفع ملف PDF فقط.");
                        model.LibrarySectionItems = _context.Set<LibrarySection>()
                            .Select(s => new SelectListItem
                            {
                                Value = s.SectionId.ToString(),
                                Text = s.Name
                            }).ToList();
                        return View(model);
                    }

                    // مسار مجلد رفع ملفات PDF
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "PDF");
                    Directory.CreateDirectory(uploadsFolder);

                    // اسم ملف فريد
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    // حذف الملف القديم إذا موجود
                    if (!string.IsNullOrEmpty(book.BookPath))
                    {
                        var oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, book.BookPath.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    book.BookPath = "/PDF/" + uniqueFileName;

                    // استخدم PdfLibCore لقراءة عدد الصفحات
                    using (var document = new PdfDocument(System.IO.File.ReadAllBytes(filePath)))
                    {
                        book.PageCount = document.Pages.Count;
                    }
                }

                book.Title = model.Name;
                book.Description = model.Description;
                book.LibrarySectionId = model.LibrarySectionId;

                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.BookId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                // حذف ملف الكتاب إن وجد
                if (!string.IsNullOrEmpty(book.BookPath))
                {
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, book.BookPath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return Content("done");
            }

            return Content("fail");
        }


        private bool BookExists(short id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
  
    }
}
