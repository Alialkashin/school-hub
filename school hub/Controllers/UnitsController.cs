using Microsoft.AspNetCore.Mvc;
using school_hub.Data;

namespace school_hub.Controllers
{
    public class UnitsController : Controller
    {
        private readonly AppDBContext _context;
        public UnitsController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Details(int? id)
        {
            throw new NotImplementedException();
        }
        
    }
}