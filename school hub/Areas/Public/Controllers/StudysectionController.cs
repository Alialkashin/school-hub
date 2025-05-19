using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Public.Controllers
{
    [Area("Public")]
    public class StudySectionController : Controller
    {
        private readonly AppDBContext _context;
        public StudySectionController(AppDBContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            var studysection1 = await _context.Sections
                .OfType<StudySection>()
                .ToListAsync();
            return View(studysection1);
        }

     
        public async  Task<IActionResult> Details(short? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var studySection = await _context.Sections
        .OfType<StudySection>()
        .Include(s => s.StudyPlans)
        .FirstOrDefaultAsync(s => s.SectionId == id);
            if (studySection == null)
            {
                return NotFound();
            }
            //var stadyplans =studySection.StudyPlans.ToList();
            return View(studySection);
        }
    }
}
