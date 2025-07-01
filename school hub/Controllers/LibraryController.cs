using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Controllers
{
    public class LibraryController : Controller
    {
        private readonly AppDBContext _context;
        public LibraryController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Sections()
        {
            List<LibrarySection> librarySections = _context.Sections.OfType<LibrarySection>().ToList();
            return View(librarySections);
        }
        public async Task<IActionResult> GetSectionBooks(int id)
        {
            var section = await _context.Sections
                .OfType<LibrarySection>()
                .Include(s => s.Books)
                .FirstOrDefaultAsync(s => s.SectionId == id);

            if (section == null)
                return NotFound();

            // ÊÃßÏ Ãä ÇáßÊÈ ÝÚáÇð ÊÊÈÚ ááÞÓã ÇáãÍÏÏ
            section.Books = section.Books.Where(b => b.LibrarySectionId == id).ToList();

            return View(section);
        }


        public IActionResult ShowBook(int bookId)
        {
            Book? book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
            if (book == null)
            {
                return NotFound();
            }

            return View(bookId);
        }
    }
}