using Microsoft.AspNetCore.Mvc;
using school_hub.Data;
namespace school_hub.Controllers
{
    public class StudyPlansController : Controller
    {
        private readonly AppDBContext _context;

        public StudyPlansController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Details(int? id)
        {
            throw new NotImplementedException();
        }

    }
}