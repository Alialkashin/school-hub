using Microsoft.AspNetCore.Mvc;
using school_hub.Models;

namespace school_hub.Areas.Tetcher.Controllers
{
    public class UnitsController : Controller
    {


        public IActionResult Index(int Id)
        {
            return View(new List<Unit>());
        }
        
        
    }
}