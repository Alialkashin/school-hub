using Microsoft.AspNetCore.Mvc;
using school_hub.Data;

namespace school_hub.Areas.Student.Controllers
{
    public class SubsecriptionsController : Controller
    {
        private readonly AppDBContext _context;
        public SubsecriptionsController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(int StudentId)
        {
            throw new NotImplementedException();
        }
        public IActionResult AddCourse(int studyPlanId)
        {
            
            throw new NotImplementedException();
        }

        
    }
}