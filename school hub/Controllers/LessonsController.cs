using Microsoft.AspNetCore.Mvc;
using school_hub.Data;

namespace school_hub.Controllers
{
    public class LessonsController : Controller
    {
        private readonly AppDBContext _context;
        public LessonsController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Details(short? id)
        {
            throw new NotImplementedException();
        }
        
    }
}