using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using school_hub.Areas.Adminstration.ViewModels;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    public class SubjectsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _hostingEnvironmentsubject;

        public SubjectsController(AppDBContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironmentsubject = hostingEnvironment;
        }

        // GET: Adminstration/Subjects
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Subjects.Include(s => s.StudyPlan).Include(s => s.Teacher);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Adminstration/Subjects/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .Include(s => s.StudyPlan)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Adminstration/Subjects/Create
        public IActionResult Create()
        {

            InputSubjectViewModel inputsubjectViewModel = new InputSubjectViewModel();

            inputsubjectViewModel.StudyPlans = _context.StudyPlans
                .Select(s => new SelectListItem
                {
                    Value = s.StudyPlanId.ToString(),
                    Text = s.Name
                }).ToList();

            inputsubjectViewModel.Teacher = _context.Users
                .OfType<Teacher>()
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.UserName
                }).ToList();

            return View(inputsubjectViewModel);




        }



        // POST: Adminstration/Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputSubjectViewModel model)
        {
           
                if (ModelState.IsValid)
                {
                    Subject sub = new Subject();

                    if (model.File != null && model.File.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_hostingEnvironmentsubject.WebRootPath, "images/subject/");
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        Directory.CreateDirectory(uploadsFolder);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.File.CopyToAsync(fileStream);
                        }

                        sub.ImagePath = "/images/subject/" + uniqueFileName;
                    }

                    sub.Name = model.Name;
                    sub.Description = model.Description;
                    sub.StudyPlanId = model.StudyPlanId;
                    sub.TotalDuration = model.TotalDuration;
                    sub.TeacherId = model.TeacherId;

                    _context.Subjects.Add(sub);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                // إعادة تعبئة القائمة عند الخطأ
              

                model.Teacher = _context.Users
                    .Where(u => u.UserType == enUserType.Teacher)
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.UserName
                    }).ToList();

                return View(model);
            }
        // GET: Adminstration/Subjects/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .Include(s => s.StudyPlan)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Adminstration/Subjects/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                if (!string.IsNullOrEmpty(subject.ImagePath))
                {
                    var filePath = Path.Combine(_hostingEnvironmentsubject.WebRootPath, subject.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
                return Content("done");
            }

            return Content("fail");
        }

        // GET: Adminstration/Subjects/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
                return NotFound();

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                return NotFound();

            var model = new InputSubjectViewModel
            {
          
                Name = subject.Name,
                Description = subject.Description,
                StudyPlanId = subject.StudyPlanId,
                TotalDuration = subject.TotalDuration,
                TeacherId = subject.TeacherId,
                ExistingImagePath = subject.ImagePath,
                StudyPlans = _context.StudyPlans.Select(s => new SelectListItem
                {
                    Value = s.StudyPlanId.ToString(),
                    Text = s.Name
                }).ToList(),
                Teacher = _context.Users.OfType<Teacher>().Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.UserName
                }).ToList()
            };

            return View(model);
        }


        // POST: Adminstration/Subjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id ,InputSubjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.StudyPlans = _context.StudyPlans.Select(s => new SelectListItem
                {
                    Value = s.StudyPlanId.ToString(),
                    Text = s.Name
                }).ToList();

                model.Teacher = _context.Users.OfType<Teacher>().Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.UserName
                }).ToList();

                return View(model);
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                return NotFound();

         
            if (model.File != null && model.File.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironmentsubject.WebRootPath, "images/subject/");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                Directory.CreateDirectory(uploadsFolder);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                }

             
                if (!string.IsNullOrEmpty(subject.ImagePath))
                {
                    var oldImagePath = Path.Combine(_hostingEnvironmentsubject.WebRootPath, subject.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                subject.ImagePath = "/images/subject/" + uniqueFileName;
            }

           
            subject.Name = model.Name;
            subject.Description = model.Description;
            subject.StudyPlanId = model.StudyPlanId;
            subject.TotalDuration = model.TotalDuration;
            subject.TeacherId = model.TeacherId;

            _context.Update(subject);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private bool SubjectExists(short id)
        {
            return _context.StudyPlans.Any(e => e.StudyPlanId == id);
        }

    }
}
