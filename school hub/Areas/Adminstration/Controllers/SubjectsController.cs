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
        public IActionResult Index()
        {
            List<Subject>? subjects = _context.Subjects.Include(s => s.StudyPlan).Include(s => s.Teacher).ToList();
            return View(subjects);
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

            inputsubjectViewModel.Teachers = _context.Users
                .OfType<school_hub.Models.Teacher>()
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
                        var uploadsFolder = Path.Combine(_hostingEnvironmentsubject.WebRootPath, "images/subjects/");
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        Directory.CreateDirectory(uploadsFolder);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.File.CopyToAsync(fileStream);
                        }

                        sub.ImagePath = "/images/subjects/" + uniqueFileName;
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
              

                model.Teachers = _context.Users
                    .Where(u => u.UserType == enUserType.Teacher)
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.UserName
                    }).ToList();

                return View(model);
            }
[HttpGet]
        public async Task<IActionResult> Edit(short ? id)
        {
            Subject? subject = await _context.Subjects.FirstOrDefaultAsync(s=> s.SubjectId == id);
            if (subject == null)
                return NotFound();
                
            InputSubjectViewModel model = new InputSubjectViewModel()
            {
                Name = subject.Name,
                SubjectId = subject.SubjectId,
                Description = subject.Description,
                ExistingImagePath = subject.ImagePath,
                StudyPlans = _context.Set<StudyPlan>().Select(s => new SelectListItem
                {
                    Value = s.StudyPlanId.ToString(),
                    Text = s.Name
                }).ToList(),
                Teachers = _context.Set<school_hub.Models.Teacher>().Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.UserName
                }).ToList(),
                TotalDuration = subject.TotalDuration
                       
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(InputSubjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teachers = _context.Set<User>().Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.UserName
                }).ToList();
                model.StudyPlans = _context.Set<StudyPlan>().Select(s => new SelectListItem()
                {
                    Value = s.StudyPlanId.ToString(),
                    Text = s.Name
                }).ToList();
                return View(model);
            }

            var subject = await _context.Subjects.FindAsync(model.SubjectId);
            if (subject == null) { return NotFound(); }


            try
            {
                if (model.File != null && model.File.Length > 0)
                {
                    var uploadsfolder = Path.Combine(_hostingEnvironmentsubject.WebRootPath, "images/subjects/");
                    var uniquefilename = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    var filepath = Path.Combine(uploadsfolder, uniquefilename);
                    Directory.CreateDirectory(uploadsfolder);
                    using (var filestream = new FileStream(filepath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(filestream);
                    }
                    if (!string.IsNullOrEmpty(subject.ImagePath))
                    {
                        var oldimagefile = Path.Combine(_hostingEnvironmentsubject.WebRootPath, subject.ImagePath.TrimStart('/'));

                        if (System.IO.File.Exists(oldimagefile))
                        {
                            System.IO.File.Delete(oldimagefile);
                        }
                    }
                    subject.ImagePath = "/images/subjects/" + uniquefilename;
                }

                subject.Name = model.Name;
                subject.Description = model.Description;
                subject.StudyPlanId = model.StudyPlanId;
                subject.TeacherId = model.TeacherId;
                subject.TotalDuration = model.TotalDuration;



                _context.Subjects.Update(subject);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Subjects.Any(s => s.SubjectId == model.SubjectId))
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
        public async Task<IActionResult> Deleteconfirmed(short id)
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

    }
}
