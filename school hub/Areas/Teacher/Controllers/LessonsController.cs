using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;
namespace school_hub.Areas.Tetchers.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = nameof(enUserType.Teacher))]
    public class LessonsController : Controller
    {
        private readonly AppDBContext _context;
        public LessonsController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(short unitId)
    {
        var lessons = await _context.Lessons
            .Where(l => l.UnitId == unitId)
            .Include(l => l.Exam)
            .Include(l => l.Videos)
            .ToListAsync();

        ViewBag.UnitId = unitId;
        return View(lessons);
    }



    }
}