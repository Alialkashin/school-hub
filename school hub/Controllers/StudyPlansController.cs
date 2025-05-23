using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;
namespace school_hub.Controllers
{
    public class StudyPlansController : Controller
    {
        private readonly AppDBContext _context;
        public StudyPlansController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Details(int id)
        {
            StudyPlan? studyPlan = _context.StudyPlans
            .Include(sp => sp.Subjects)
          .FirstOrDefault(sp => sp.StudyPlanId == id);
          
            if (studyPlan == null)
            {
                
                return NotFound();
            }
            return View(studyPlan);

        }

    }
}