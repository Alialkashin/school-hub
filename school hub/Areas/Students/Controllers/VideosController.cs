using Microsoft.AspNetCore.Mvc;
using school_hub.Data;

namespace school_hub.Students.Controllers
{
    public class VideosController : Controller
    {
        private readonly AppDBContext _context;
        public VideosController(AppDBContext context)
        {
            _context = context;
        }
        
        public IActionResult Index(int LessonId)
        {
            throw new NotImplementedException();
        }

    }
    
}