using Microsoft.AspNetCore.Mvc;
using school_hub.Areas.Adminstration.ViewModels;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Adminstration.Controllers
{
    public class StudySectionController : Controller
    {
        private readonly ILogger<StudySection> _logger;
        private readonly AppDBContext _context;
        public StudySectionController(ILogger<StudySection> logger , AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(InputStudySectionViewModel vmStudySection)
        {
            if(string.IsNullOrWhiteSpace(vmStudySection.Name) || string.IsNullOrWhiteSpace(vmStudySection.Description))
            {
                return View(vmStudySection);
            }
            var studySection = new StudySection(){
                Name = vmStudySection.Name,
                Description = vmStudySection.Description,
                SectionType = enSectionType.StudySection,

            };
            var result = await _context.Sections.AddAsync(studySection);
            return RedirectToAction("Index");


        }

        
    }
}