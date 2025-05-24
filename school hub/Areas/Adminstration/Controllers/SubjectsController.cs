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
                .Where(u => u.UserType == enUserType.Teacher)
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

        

        // GET: Adminstration/Subjects/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            ViewData["StudyPlanId"] = new SelectList(_context.StudyPlans, "StudyPlanId", "StudyPlanId", subject.StudyPlanId);
            ViewData["TeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "Id", subject.TeacherId);
            return View(subject);
        }

        // POST: Adminstration/Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("SubjectId,TotalDuration,TeacherId,StudyPlanId,Name,Description,ImagePath")] Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.SubjectId))
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
            ViewData["StudyPlanId"] = new SelectList(_context.StudyPlans, "StudyPlanId", "StudyPlanId", subject.StudyPlanId);
            ViewData["TeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "Id", subject.TeacherId);
            return View(subject);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(short id)
        {
            return _context.Subjects.Any(e => e.SubjectId == id);
        }
    }
}
