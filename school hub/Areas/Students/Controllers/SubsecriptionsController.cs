using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_hub.Data;
using school_hub.Models;
using school_hub.Areas.Students.ViewModels;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace school_hub.Areas.Students.Controllers
{

    [Authorize(Roles = nameof(enUserType.Student))]
    public class SubsecriptionsController : Controller
    {
        public int StudentId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        private readonly AppDBContext _context;
        public SubsecriptionsController(AppDBContext context)
        {
            _context = context;

        }

        public IActionResult MySubscriptions()
        {
            var studentsub = _context.StudentSubscriptions
                .Where(ss => ss.StudentId == StudentId && ss.PaymentStatus != enPaymentStatus.Complete)
                .ToList();

            var sub = studentsub.Select(itme => new SubsecriptionViewModel
            {
                studyPlanId = itme.PlanId,
                paymentStatus = itme.PaymentStatus,
                StudyPlanName = itme.StudyPlan.Name,
                StudySectionName = itme.StudyPlan.StudySection.Name
            }).ToList();

            // تجميع البيانات بحسب القسم
            var grouped = sub
                .GroupBy(s => s.StudySectionName)
                .Select(g => new StudySectionWithPlansViewModel
                {
                    StudySectionName = g.Key,
                    StudyPlans = g.ToList()
                }).ToList();

            return View(grouped);
        }
        public IActionResult MyCourses()
        {
            var studentsub = _context.StudentSubscriptions
                .Where(ss => ss.StudentId == StudentId && ss.PaymentStatus != enPaymentStatus.Complete)
                .ToList();

            var sub = studentsub.Select(itme => new SubsecriptionViewModel
            {
                studyPlanId = itme.PlanId,
                paymentStatus = itme.PaymentStatus,
                StudyPlanName = itme.StudyPlan.Name,
                StudySectionName = itme.StudyPlan.StudySection.Name
            }).ToList();

            // تجميع البيانات بحسب القسم
            var grouped = sub
                .GroupBy(s => s.StudySectionName)
                .Select(g => new StudySectionWithPlansViewModel
                {
                    StudySectionName = g.Key,
                    StudyPlans = g.ToList()
                }).ToList();

            return View(grouped);
        }

        public IActionResult CourseDetails(short id)
        {
            StudyPlan? studyPlan = _context.StudyPlans
             .Include(sp => sp.Subjects)
           .FirstOrDefault(sp => sp.StudyPlanId == id);

            if (studyPlan == null)
            {

                return NotFound();
            }
            return View(studyPlan);
        }
        public IActionResult SubjectDetails(short id)
        {
            Subject? subject = _context.Subjects.Include(s => s.Units).FirstOrDefault(s => s.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);


        }
        public IActionResult UnitLessons(short unitId)
        {

            // جلب الدروس مع علاقاتها
            var lessons = _context.Lessons
                .Where(l => l.UnitId == unitId)
                .Include(l => l.Ratings)
                .Include(l => l.Exam)
                    .ThenInclude(e => e.StudentExams)
                .ToList();

            // تجهيز ViewModel
            var lessonViewModels = new List<LessonViewModel>();

            foreach (var lesson in lessons.OrderBy(l => l.LessonNo))
            {
                // حساب متوسط التقييم
                double avgRating = lesson.Ratings.Any() ? lesson.Ratings.Average(r => r.RatingValue) : 0;

                // تحديد إذا كان الدرس مفتوحًا
                bool isFirstLesson = lesson.LessonNo == 1;
                bool isUnlocked = false;

                if (isFirstLesson)
                {
                    isUnlocked = true;
                }
                else
                {
                    var previousLesson = lessons.FirstOrDefault(l => l.LessonId == lesson.PreviousLesson);

                    if (previousLesson != null)
                    {
                        var previousExam = previousLesson.Exam;

                        if (previousExam != null)
                        {
                            var studentExam = previousExam.StudentExams.FirstOrDefault(se => se.StudentId == StudentId);

                            if (studentExam != null)
                            {
                                // هنا يمكنك إضافة شروط إضافية إذا كان يجب أن ينجح الطالب في الامتحان، مثل عدد الإجابات الصحيحة.
                                isUnlocked = true;
                            }
                        }
                    }
                }

                lessonViewModels.Add(new LessonViewModel
                {
                    LessonId = lesson.LessonId,
                    LessonNo = lesson.LessonNo,
                    Title = lesson.Title,
                    AverageRating = Math.Round(avgRating, 1),
                    IsUnlocked = isUnlocked
                });
            }

            return View(lessonViewModels);

        }
    }
}