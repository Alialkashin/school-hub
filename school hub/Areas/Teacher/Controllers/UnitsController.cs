using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using school_hub.Data;
using school_hub.Models;
using Microsoft.EntityFrameworkCore;
using school_hub.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using school_hub.Areas.Teachers.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Authorization;

namespace school_hub.Areas.Tetchers.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles =nameof(enUserType.Teacher))]
    public class UnitsController : Controller
{
    private int TeacherId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    private readonly AppDBContext _context;
    private readonly IWebHostEnvironment _env;

    public UnitsController(AppDBContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IActionResult> Index(short? subjectId)
{
    var units = _context.Units
        .Include(u => u.Subject)
        .AsQueryable();

            // الفلترة
    if (subjectId.HasValue)
    {
        if (!_context.Subjects.Any(s => s.TeacherId == TeacherId))
        {
            return Forbid();
        }
        units = units.Where(u => u.SubjectId == subjectId);
        
    }

    ViewBag.Subjects = await _context.Subjects.Where(s => s.TeacherId == TeacherId).ToListAsync();

    return View(await units.ToListAsync());
}


    public IActionResult Create()
    {
        return View("UnitForm", PrepareViewModel());
    }

    // POST: Units/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UnitFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Subjects = GetSubjects();
            return View("UnitForm", model);
        }

        var unit = new Unit
        {
            Name = model.Name,
            Description = model.Description,
            SubjectId = model.SubjectId,
            ImagePath = await SaveImageAsync(model.ImageFile)
        };

        _context.Units.Add(unit);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // GET: Units/Edit/5
    public async Task<IActionResult> Edit(short id)
    {
        var unit = await _context.Units.FindAsync(id);
        if (unit == null)
            return NotFound();

        var model = new UnitFormViewModel
        {
            UnitId = unit.UnitId,
            Name = unit.Name,
            Description = unit.Description,
            SubjectId = unit.SubjectId,
            ExistingImagePath = unit.ImagePath,
            Subjects = GetSubjects()
        };

        return View("UnitForm", model);
    }

    // POST: Units/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UnitFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Subjects = GetSubjects();
            return View("UnitForm", model);
        }

        var unit = await _context.Units.FindAsync(model.UnitId);
        if (unit == null)
            return NotFound();

        unit.Name = model.Name;
        unit.Description = model.Description;
        unit.SubjectId = model.SubjectId;

        if (model.ImageFile != null)
        {
            if (!string.IsNullOrEmpty(unit.ImagePath))
            {
                var oldPath = Path.Combine(_env.WebRootPath, unit.ImagePath);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            unit.ImagePath = await SaveImageAsync(model.ImageFile);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(short id)
    {
        var unit = await _context.Units.FindAsync(id);
        if (unit == null) return NotFound();

        if (!string.IsNullOrEmpty(unit.ImagePath))
        {
            var path = Path.Combine(_env.WebRootPath, "images", "units", unit.ImagePath);
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
        }

        _context.Units.Remove(unit);
        await _context.SaveChangesAsync();
        return Ok();
    }

    private async Task<string?> SaveImageAsync(IFormFile? file)
    {
        if (file == null) return null;

        var uploadsFolder = Path.Combine("images", "Units");
        var wwwRoot = Path.Combine(_env.WebRootPath, uploadsFolder);
        Directory.CreateDirectory(wwwRoot);

        var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(wwwRoot, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Combine("/",uploadsFolder, uniqueFileName).Replace("\\", "/");
    }
    private List<SelectListItem> GetSubjects()
    {
        return _context.Subjects.Where(s => s.TeacherId == TeacherId)
            .Select(s => new SelectListItem
            {
                Value = s.SubjectId.ToString(),
                Text = s.Name
            })
            .ToList();
    }

    private UnitFormViewModel PrepareViewModel(UnitFormViewModel? vm = null)
    {
        vm ??= new UnitFormViewModel();
            vm.Subjects = GetSubjects();
        return vm;
    }
}

    
}