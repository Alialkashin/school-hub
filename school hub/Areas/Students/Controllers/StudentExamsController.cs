using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Areas.Students.ViewModels;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Students.Controllers
{
    [Authorize(Roles = nameof(enUserType.Student))]
    public class StudentExamsController : Controller
    {
        private readonly AppDBContext _context;
        public StudentExamsController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Start(short examId)
        {
            var exam = await _context.Exams
                .Include(e => e.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(e => e.ExamId == examId);

            if (exam == null)
                return NotFound();

            var viewModel = new ExamStartViewModel
            {
                ExamId = exam.ExamId,
                ExamTitle = $"امتحان الدرس رقم {exam.ExamId}",
                Questions = exam.Questions.Select(q => new ExamQuestionViewModel
                {
                    QuestionId = q.QuestionId,
                    Text = q.Text,
                    Answers = q.Answers.Select(a => new AnswerOptionViewModel
                    {
                        AnswerId = a.AnswerId,
                        Text = $"إجابة {a.AnswerId}" // يمكن عرض محتوى لاحقًا
                    }).ToList()
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(short ExamId, Dictionary<int, int> Answers)
        {
            // أنشئ سجل StudentExam
            var studentExam = new StudentExam
            {
                ExamId = ExamId,
                StudentId = 1, // من الجلسة
                ExamDate = DateOnly.FromDateTime(DateTime.Now),
                TimeToComlete = 0
            };
            _context.StudentExams.Add(studentExam);
            await _context.SaveChangesAsync();

            foreach (var pair in Answers)
            {
                var studentAnswer = new StudentAnswer
                {
                    StudentExamId = studentExam.StudentExamId,
                    AnswerId = pair.Value
                };
                _context.StudentAnswers.Add(studentAnswer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Result", new { id = studentExam.StudentExamId });
        }

        public async Task<IActionResult> Result(int id)
        {
            var exam = await _context.StudentExams
                .Include(e => e.StudentAnswers)
                    .ThenInclude(sa => sa.Answer)
                .Include(e => e.Exam)
                .FirstOrDefaultAsync(e => e.StudentExamId == id);

            if (exam == null)
                return NotFound();

            int correctAnswers = exam.StudentAnswers.Count(sa => sa.Answer.IsCorrect);
            bool isPassed = correctAnswers >= exam.Exam.PassingScore;

            ViewBag.Score = correctAnswers;
            ViewBag.IsPassed = isPassed;
            ViewBag.Total = exam.Exam.Questions.Count;

            return View(); 
        }
    }
}

