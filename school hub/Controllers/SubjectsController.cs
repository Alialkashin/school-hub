using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly AppDBContext _context;

        public SubjectsController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Details(short id)
        {
            Subject? subject = _context.Subjects.Include(s => s.Units).FirstOrDefault(s => s.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }
        
    }
}