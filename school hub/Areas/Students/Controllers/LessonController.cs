using Microsoft.AspNetCore.Mvc;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Students.Controllers
{
    public class LessonController : Controller
       
    {
        private readonly AppDBContext _context;
        public LessonController(AppDBContext context) 
        {
            _context = context;
        }
        public IActionResult LessonDetails(short id)
        {
            //Lesson lesson = _context.Lessons.FirstOrDefault(l => l.LessonId == id);
            return View();
        }
    }
}
