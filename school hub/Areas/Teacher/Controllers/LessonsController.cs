using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Areas.Teacher.ViewModels;
using school_hub.Data;
using school_hub.Models;
namespace school_hub.Areas.Tetchers.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = nameof(enUserType.Teacher))]
    public class LessonsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;
        public LessonsController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public int TeacherId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        public async Task<IActionResult> Index(short unitId)
        {
            var lessons = await _context.Lessons
                .Where(l => l.UnitId == unitId)
                .ToListAsync();

            ViewBag.UnitId = unitId;
            return View(lessons);
        }
        public async Task<IActionResult> Create(short unitId)
        {
            var sub = _context.Units.Where(u => u.Subject.TeacherId == TeacherId && u.UnitId == unitId).FirstOrDefault();
            if (sub == null)
            {
                return NotFound();
            }
            var model = new CreateLessonViewModel
            {
                UnitId = unitId
            };
            return View("Upsert", model); // نستخدم نفس الواجهة
        }


        // عرض النموذج - للتعديل
        public async Task<IActionResult> Edit(short id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
                return NotFound();

            var model = new CreateLessonViewModel
            {
                LessonId = lesson.LessonId,
                UnitId = lesson.UnitId,
                LessonNo = lesson.LessonNo,
                Title = lesson.Title
            };

            return View("Upsert", model); // نستخدم نفس الواجهة
        }


        // استقبال النموذج - إنشاء أو تعديل
        [HttpPost]
        public async Task<IActionResult> Upsert(CreateLessonViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.LessonId == null)
            {
                // إنشاء
                var newLesson = new Lesson
                {
                    Title = model.Title,
                    UnitId = model.UnitId
                };

                var maxLessonNo = await _context.Lessons
                    .Where(l => l.UnitId == model.UnitId)
                    .Select(l => (byte?)l.LessonNo)
                    .MaxAsync();

                if (maxLessonNo == null)
                {
                    newLesson.LessonNo = 1;
                    newLesson.PreviousLesson = null;
                }
                else
                {
                    newLesson.LessonNo = (byte)(maxLessonNo + 1);
                    newLesson.PreviousLesson = await _context.Lessons
                        .Where(l => l.UnitId == model.UnitId && l.LessonNo == maxLessonNo)
                        .Select(l => (short?)l.LessonId)
                        .FirstOrDefaultAsync();
                }

                await _context.Lessons.AddAsync(newLesson);
            }
            else
            {
                // تعديل
                var existingLesson = await _context.Lessons.FindAsync((short)model.LessonId.Value);
                if (existingLesson == null)
                    return NotFound();

                existingLesson.Title = model.Title;
                _context.Lessons.Update(existingLesson);
            }

            await _context.SaveChangesAsync();
            return Redirect("/Teacher/Lessons/index?unitId=" + model.UnitId);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // البحث عن الدرس مع تضمين الكيانات المرتبطة
                var lesson = await _context.Lessons
                    .Include(l => l.Videos) // تضمين الفيديوهات المرتبطة
                    .FirstOrDefaultAsync(l => l.LessonId == id);

                if (lesson == null)
                {
                    return NotFound();
                }

                // حذف الفيديوهات المرتبطة أولاً
                if (lesson.Videos != null && lesson.Videos.Any())
                {
                    foreach (var video in lesson.Videos)
                    {
                        // حذف ملف الفيديو من نظام الملفات
                        if (!string.IsNullOrEmpty(video.VideoPath))
                        {
                            var filePath = Path.Combine(_env.WebRootPath, video.VideoPath.TrimStart('/'));
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                    }
                    _context.Videos.RemoveRange(lesson.Videos);
                }

                // حذف الدرس
                _context.Lessons.Remove(lesson);

                // حفظ التغييرات
                var result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    throw new Exception("فشل في حذف الدرس");
                }

                await transaction.CommitAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // يمكن تسجيل الخطأ هنا
                return StatusCode(500, "حدث خطأ أثناء محاولة حذف الدرس");
            }
        }




 
    }
}