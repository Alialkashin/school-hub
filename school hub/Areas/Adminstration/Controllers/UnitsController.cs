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

namespace school_hub.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    public class UnitsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _hostingEnvironmentunit;

        public UnitsController(AppDBContext context,IWebHostEnvironment _hostingEnvironment)
        {
            _context = context;
            _hostingEnvironmentunit = _hostingEnvironment;

        }

        // GET: Adminstration/Units
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Units.Include(u => u.Subject);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Adminstration/Units/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .Include(u => u.Subject)
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: Adminstration/Units/Create
        public IActionResult Create()
        {
            InputUnitViewModel inputUnitViewModel = new InputUnitViewModel();
            inputUnitViewModel.Subjects =  _context.Subjects
            .Select(s => new SelectListItem
            {
                Value = s.SubjectId.ToString(), 
                Text = s.Name
            })
            .ToList();

            return View(inputUnitViewModel);
        }

        // POST: Adminstration/Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputUnitViewModel model)
        {
            if (ModelState.IsValid)
            {Unit un=new Unit();
                if (model.File != null && model.File.Length > 0)
                { 
                    
                    var uploadsFolder = Path.Combine(_hostingEnvironmentunit.WebRootPath, "images/Units/");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    Directory.CreateDirectory(uploadsFolder);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    un.ImagePath = "/images/Units/" + uniqueFileName;
                }
                un.Name= model.Name;
                un.Description= model.Description;
                un.SubjectId = model.SubjectId;



                _context.Units.Add(un);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            return View(model);
        }

        // GET: Adminstration/Units/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", unit.SubjectId);
            return View(unit);
        }

        // POST: Adminstration/Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("UnitId,SubjectId,Name,Description,ImagePath")] Unit unit)
        {
            if (id != unit.UnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitExists(unit.UnitId))
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
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", unit.SubjectId);
            return View(unit);
        }

        // GET: Adminstration/Units/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .Include(u => u.Subject)
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: Adminstration/Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit != null)
            {
                _context.Units.Remove(unit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitExists(short id)
        {
            return _context.Units.Any(e => e.UnitId == id);
        }
    }
}
