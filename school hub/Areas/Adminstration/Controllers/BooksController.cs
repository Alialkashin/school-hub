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
    public class BooksController : Controller
    {
        private readonly AppDBContext _context;

        public BooksController(AppDBContext context)
        {
            _context = context;
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
            ViewData["LibrarySectionId"] = new SelectList(_context.Set<LibrarySection>(), "SectionId", "SectionId");
            return View();
        }

        // POST: Adminstration/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,LibrarySectionId,Title,Description,BookPath")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibrarySectionId"] = new SelectList(_context.Set<LibrarySection>(), "SectionId", "SectionId", book.LibrarySectionId);
            return View(book);
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
        public async Task<IActionResult> Edit(short id, [Bind("BookId,LibrarySectionId,Title,Description,BookPath")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["LibrarySectionId"] = new SelectList(_context.Set<LibrarySection>(), "SectionId", "SectionId", book.LibrarySectionId);
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
    }
}
