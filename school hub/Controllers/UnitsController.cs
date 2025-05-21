using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Controllers
{
    public class UnitsController : Controller
    {
        private readonly AppDBContext _context;
        public UnitsController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Details(int id)
        {
            Unit? unit = _context.Units.FirstOrDefault(u => u.UnitId == id);
            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }
        
    }
}