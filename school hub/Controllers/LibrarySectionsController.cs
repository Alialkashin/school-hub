using Microsoft.AspNetCore.Mvc;
using school_hub.Data;

namespace school_hub.Controllers
{
    public class LibrarySectionsController : Controller
    {
        private readonly AppDBContext _context;
        public LibrarySectionsController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            throw new NotImplementedException();
        }
        public IActionResult Details(int sectionId)
        {
            throw new NotImplementedException();
        }

    }
}