using Microsoft.AspNetCore.Mvc;
using school_hub.Data;
using school_hub.Models;
using school_hub.ViewModels;

namespace school_hub.Controllers
{
    public class LessonsController : Controller
    {
        private readonly AppDBContext _context;
        public LessonsController(AppDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetUnitLessons(short unitId)
        {
            ICollection<Lesson> lessons = _context.Lessons.Where(l => l.UnitId == unitId).ToList();
            List<LessonViewModel> lessonsViewModel = new List<LessonViewModel>();
            foreach (var item in lessons)
            {
                lessonsViewModel.Add(new LessonViewModel()
                {
                    id = item.LessonId,
                    LessonNo = item.LessonNo,
                    Title = item.Title

                });
            }
            return Ok(lessonsViewModel);
        }
        public IActionResult Details(short? id)
        {
            throw new NotImplementedException();
        }
        
    }
}