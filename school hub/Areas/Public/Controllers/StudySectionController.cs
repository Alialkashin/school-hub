using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            var sections = _context.Sections.Where(s => s.SectionType == enSectionType.StudySection).ToList();
            return View(sections);
        }
        public IActionResult Details(int id)
        {
            var section = (StudySection)_context.Sections.Find(id);
            return View(section);
        }
    }
}
