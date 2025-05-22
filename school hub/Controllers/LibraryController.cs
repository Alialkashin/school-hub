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
        public IActionResult GetSectionBooks(int sectionId)
        {
            LibrarySection? librarySection = _context.Sections.OfType<LibrarySection>().Include(ls => ls.Books).FirstOrDefault();
            if (librarySection == null)
            {
                return NotFound();
            }
            return View(librarySection);
            
        }

    }
}