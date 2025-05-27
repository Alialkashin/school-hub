using Microsoft.AspNetCore.Mvc;
using school_hub.Data;
namespace school_hub.Areas.Tetcher.Controllers
{
    public class LessonController : Controller
    {
        private readonly AppDBContext _context;
        public LessonController(AppDBContext context)
        {
            _context = context;
        }

        

    }
}