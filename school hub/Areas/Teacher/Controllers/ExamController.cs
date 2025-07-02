using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Areas.Teacher.ViewModels;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Teacher.Controllers
{
    [Route("Exam")]
[Area("Teacher")]
public class ExamController : Controller
{
    private readonly AppDBContext _context;

    public ExamController(AppDBContext context)
    {
        _context = context;
    }
        [HttpGet("Manage/{lessonId}")]
        public IActionResult Manage(short lessonId)
        {
            // التحقق من وجود اختبار لهذا الدرس
            var exam = _context.Exams.FirstOrDefault(e => e.LessonId == lessonId);

            // إذا لم يوجد اختبار، نقوم بإنشاء واحد جديد
            if (exam == null)
            {
                exam = new Exam()
                {
                    LessonId = lessonId,
                    ExamTime = 10,    // الوقت الافتراضي للاختبار (10 دقائق)
                    PassingScore = 50   // النسبة المئوية الافتراضية للنجاح (50%)
                };

                _context.Exams.Add(exam);
                _context.SaveChanges();
            }

            // إنشاء نموذج العرض
            var viewModel = new ManageExamViewModel
            {
                LessonId = lessonId,
                ExamId = exam.ExamId
            };

            return View("ManagementExam", viewModel);
        }


    // الحصول على examId من lessonId
        [HttpGet("GetExamIdByLesson/{lessonId}")]
    public async Task<IActionResult> GetExamIdByLesson(short lessonId)
    {
        var examId = await _context.Lessons
            .Where(l => l.LessonId == lessonId)
            .Select(l => l.ExamId)
            .FirstOrDefaultAsync();

        return Json(new { examId });
    }

    // الحصول على الأسئلة مع إجاباتها
    [HttpGet("GetQuestionsByExamId/{examId}")]
    public async Task<IActionResult> GetQuestionsByExamId(short examId)
    {
        var questions = await _context.Questions
            .Where(q => q.ExamId == examId)
            .Select(q => new
            {
                questionId = q.QuestionId,
                text = q.Text,
                answers = q.Answers.Select(a => new
                {
                    text = a.Text,
                    isCorrect = a.IsCorrect
                })
            })
            .ToListAsync();

        return Json(questions);
    }

    // الحصول على سؤال معين
    [HttpGet("GetQuestionById/{questionId}")]
    public async Task<IActionResult> GetQuestionById(int questionId)
    {
        var question = await _context.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null) return NotFound();

        return Json(new
        {
            questionId = question.QuestionId,
            text = question.Text,
            answers = question.Answers.Select(a => new
            {
                text = a.Text,
                isCorrect = a.IsCorrect
            })
        });
    }

    // إضافة سؤال جديد
    [HttpPost("AddQuestion")]
    public async Task<IActionResult> AddQuestion([FromBody] QuestionDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // إنشاء السؤال
            var question = new Question
            {
                ExamId = dto.ExamId,
                Text = dto.Text
            };

            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            // إضافة الإجابات
            foreach (var answerDto in dto.Answers)
            {
                var answer = new Answer
                {
                    QuestionId = question.QuestionId,
                    Text = answerDto.Text,
                    IsCorrect = answerDto.IsCorrect
                };

                await _context.Answers.AddAsync(answer);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, "حدث خطأ أثناء إضافة السؤال");
        }
    }

    // تحديث سؤال موجود
    [HttpPut("UpdateQuestion")]
    public async Task<IActionResult> UpdateQuestion([FromBody] QuestionDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // تحديث السؤال
            var question = await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuestionId == dto.QuestionId);

            if (question == null) return NotFound();

            question.Text = dto.Text;
            _context.Questions.Update(question);

            // حذف الإجابات القديمة
            _context.Answers.RemoveRange(question.Answers);

            // إضافة الإجابات الجديدة
            foreach (var answerDto in dto.Answers)
            {
                var answer = new Answer
                {
                    QuestionId = question.QuestionId,
                    Text = answerDto.Text,
                    IsCorrect = answerDto.IsCorrect
                };

                await _context.Answers.AddAsync(answer);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, "حدث خطأ أثناء تحديث السؤال");
        }
    }

    // حذف سؤال
    [HttpDelete("DeleteQuestion/{questionId}")]
    public async Task<IActionResult> DeleteQuestion(int questionId)
    {
        var question = await _context.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null) return NotFound();

        try
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500, "حدث خطأ أثناء حذف السؤال");
        }
    }

    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public short ExamId { get; set; }
        public string Text { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }

    public class AnswerDto
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
    
    
}