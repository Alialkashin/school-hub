using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Areas.Teacher.ViewModels;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Teechers.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = nameof(enUserType.Teacher))]
    public class SubjectsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        private int TeacherId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public SubjectsController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            var subjects = await _context.Subjects
                .Include(s => s.StudyPlan)
                .Where(s => s.TeacherId == TeacherId)
                .ToListAsync();

            return View(subjects);
        }

        public async Task<IActionResult> Edit(short? id)
{
    if (id == null) return NotFound();

    var subject = await _context.Subjects.FindAsync(id);
    if (subject == null || subject.TeacherId != TeacherId) return NotFound();

    var viewModel = new EditSubjectViewModel
    {
        SubjectId = subject.SubjectId,
        Name = subject.Name,
        TotalDuration = subject.TotalDuration,
        ExistingImagePath = subject.ImagePath
    };

    return View(viewModel);
}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditSubjectViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var subject = await _context.Subjects.FindAsync(model.SubjectId);
            if (subject == null || subject.TeacherId != TeacherId) return NotFound();

            subject.Name = model.Name;
            subject.TotalDuration = model.TotalDuration;

            if (model.NewImage != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images", "subjects");
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImage.FileName);
                string filePath = Path.Combine(uploadsFolder, newFileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.NewImage.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(subject.ImagePath))
                {
                    string oldImagePath = Path.Combine(uploadsFolder, subject.ImagePath);
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                subject.ImagePath = newFileName;
            }

            _context.Update(subject);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null || subject.TeacherId != TeacherId)
                return NotFound();

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return Ok(); // لا نعيد View بل نعيد 200 OK فقط
        }
    }

}