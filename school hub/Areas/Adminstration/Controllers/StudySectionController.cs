using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    public class StudySectionController : Controller
    {
        private readonly ILogger<StudySection> _logger;
        private readonly AppDBContext _context;
        public StudySectionController(ILogger<StudySection> logger , AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return View();

            return View();
            



            
        }
        public IActionResult Index()
        {

            var StudySection = _context.Sections.Where(c => c.SectionType == enSectionType.StudySection).ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) {
                return View();
            }
            var studySection = _context.Sections.Find(id);
            return View(studySection);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudySection section)
        {
            if(string.IsNullOrWhiteSpace(section.Name) || string.IsNullOrWhiteSpace(section.Description))
            {
                return View(section);
            }
            var studySection = new StudySection(){
                Name = section.Name,
                Description = section.Description,
                SectionType = enSectionType.StudySection,

            };
            if ( studySection.SectionId == 0)
                await _context.Sections.AddAsync(studySection);
            else
                _context.Sections.Update(studySection);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }

        
    }
}