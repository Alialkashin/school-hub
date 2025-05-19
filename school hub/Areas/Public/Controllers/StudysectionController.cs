using Microsoft.AspNetCore.Mvc;
using school_hub.Data;

namespace school_hub.Areas.Public.Controllers
{
    public class StudysectionController : Controller
    {
        private readonly AppDBContext _context;
        public IActionResult Index()
        {
            return View();
        }
    }
}
