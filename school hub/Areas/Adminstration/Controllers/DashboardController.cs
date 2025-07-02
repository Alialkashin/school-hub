using Ghostscript.NET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using school_hub.Areas.Adminstration.ViewModels;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    public class DashboardController : Controller
    {

        private readonly AppDBContext _context;
       
        public DashboardController(AppDBContext context)
        {
            _context = context;
       
        }
        public async Task<IActionResult> Index()
        {
            var viewmodel = new AdminDashboardViewModel
            {
                TotalBooks = await _context.Books.CountAsync(),
                TotalLibrarySections = await _context.Sections.OfType<LibrarySection>().CountAsync(),
                TotalStudentsInPlans = await _context.StudentSubscriptions.CountAsync(),
                TotalStudyPlans = await _context.StudyPlans.CountAsync(),
                TotalStudySections = await _context.Sections.OfType<StudySection>().CountAsync(),
                TotalSubject = await _context.Subjects.CountAsync()

            };
            return View(viewmodel);


        }
    }
}
