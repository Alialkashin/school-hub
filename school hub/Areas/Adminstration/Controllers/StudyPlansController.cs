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
    public class StudyPlansController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _hostingEnvironmentstudyplans;

        public StudyPlansController(AppDBContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironmentstudyplans = hostingEnvironment;
        }

        // GET: Adminstration/StudyPlans
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.StudyPlans.Include(s => s.StudySection);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Adminstration/StudyPlans/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyPlan = await _context.StudyPlans
                .Include(s => s.StudySection)
                .FirstOrDefaultAsync(m => m.StudyPlanId == id);
            if (studyPlan == null)
            {
                return NotFound();
            }

            return View(studyPlan);
        }

        // GET: Adminstration/StudyPlans/Create
        public IActionResult Create()
        {
            InputStudyPlansViewModel model = new InputStudyPlansViewModel();
            model.StudySectionItems = _context.Set<StudySection>()
                  .Select(s => new SelectListItem
                  {
                      Value = s.SectionId.ToString(),
                      Text = s.Name
                  })
             .ToList();
            return View(model);


        }

        // POST: Adminstration/StudyPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputStudyPlansViewModel model)
        {
            if (ModelState.IsValid)
            {  
                StudyPlan studyPlan=new StudyPlan();
                if (model.File != null && model.File.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironmentstudyplans.WebRootPath, "images/studyplans/");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    Directory.CreateDirectory(uploadsFolder);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    studyPlan.ImagePath = "/images/studyplans/" + uniqueFileName;
                }
                studyPlan.Name= model.Name;
                studyPlan.Description= model.Description;
                studyPlan.StudySectionId = model.StudySectionId;
              

                _context.StudyPlans.Add(studyPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            return View(model);
        }
        // GET: Adminstration/StudyPlans/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            var study = await _context.StudyPlans.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }

            var model = new InputStudyPlansViewModel
            {
                Id = study.StudyPlanId,
                Name = study.Name,
                Description = study.Description,
                StudySectionId = study.StudySectionId,
                ExistingImagePath = study.ImagePath,
                StudySectionItems = _context.Set<StudySection>()
                       .Select(s => new SelectListItem
                       {
                           Value = s.SectionId.ToString(),
                           Text = s.Name
                       }).ToList()
            };
            return View(model);
        }

        // POST: Adminstration/StudyPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, InputStudyPlansViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                model.StudySectionItems = _context.Set<StudySection>()
                    .Select(s => new SelectListItem
                    {
                        Value = s.SectionId.ToString(),
                        Text = s.Name
                    }).ToList();
                return View(model);
            }

            var study = await _context.StudyPlans.FindAsync(id);
            if (study == null) { return NotFound(); }


            try
            {
                if (model.File != null && model.File.Length > 0)
                {
                    var uploadsfolder = Path.Combine(_hostingEnvironmentstudyplans.WebRootPath, "images/studyplans/");
                    var uniquefilename = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    var filepath = Path.Combine(uploadsfolder, uniquefilename);
                    Directory.CreateDirectory(uploadsfolder);
                    using (var filestream = new FileStream(filepath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(filestream);
                    }
                    if (!string.IsNullOrEmpty(study.ImagePath))
                    {
                        var oldimagefile = Path.Combine(_hostingEnvironmentstudyplans.WebRootPath, study.ImagePath.TrimStart('/'));

                        if (System.IO.File.Exists(oldimagefile))
                        {
                            System.IO.File.Delete(oldimagefile);
                        }
                    }
                    study.ImagePath = "/images/studyplans/" + uniquefilename;
                }
                else
                {
              
                    study.ImagePath = model.ExistingImagePath;
                }

                study.Name = model.Name;
                study.Description = model.Description;
                study.StudySectionId = model.StudySectionId;


                _context.Update(study);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!StudyPlanExists(study.StudyPlanId))
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

        // GET: Adminstration/StudyPlans/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyPlan = await _context.StudyPlans
                .Include(s => s.StudySection)
                .FirstOrDefaultAsync(m => m.StudyPlanId == id);
            if (studyPlan == null)
            {
                return NotFound();
            }

            return View(studyPlan);
        }

        // POST: Adminstration/StudyPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var studyPlan = await _context.StudyPlans.FindAsync(id);
            if (studyPlan != null)
            {
                _context.StudyPlans.Remove(studyPlan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyPlanExists(short id)
        {
            return _context.StudyPlans.Any(e => e.StudyPlanId == id);
        }
    }
}
