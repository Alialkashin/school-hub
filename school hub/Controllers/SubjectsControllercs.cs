using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Controllers
{
    public class SubjectsControllercs : Controller
    {
        private readonly AppDBContext _context;

        public SubjectsControllercs(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Details(int id)
        {
            Subject? subject = _context.Subjects.Include(s => s.Units).Include(s => s.Teacher).FirstOrDefault(s => s.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }
        
    }
}