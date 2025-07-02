using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Areas.Students.ViewModels;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Students.Controllers
{
    [Area("Students")]
    public class StudentLessonsController:Controller
    {
        private readonly AppDBContext _context;
        public StudentLessonsController(AppDBContext context)
        {
            _context = context;
        }


            public async Task<IActionResult> Details(short lessonId, int? videoId)
            {
                var lesson = await _context.Lessons
                    .Include(l => l.Videos)
                    .Include(l => l.Exam)
                    .FirstOrDefaultAsync(l => l.LessonId == lessonId);

                if (lesson == null)
                    return NotFound();

                var videos = lesson.Videos.OrderBy(v => v.VideoId).ToList();
                var selectedVideo = videos.FirstOrDefault(v => v.VideoId == videoId) ?? videos.FirstOrDefault();

                var comments = await _context.Comments
                    .Where(c => c.VideoId == selectedVideo.VideoId)
                    .Include(c => c.Student)
                    .Include(c => c.Replys)
                        .ThenInclude(r => r.Teacher)
                    .Include(c => c.Replys)
                        .ThenInclude(r => r.Student)
                    .ToListAsync();

                var viewModel = new StudentLessonViewModel
                {
                    LessonId = lessonId,
                    ExamId = lesson.ExamId,
                    Videos = videos,
                    CurrentVideo = selectedVideo,
                    Comments = comments.Select(c => new CommentViewModel
                    {
                        CommentId = c.CommentId,
                        Content = c.Content,
                        CommentDate = c.CommentDate,
                        StudentName = c.Student?.UserName ?? "طالب",
                        Replies = c.Replys?.Select(r => new ReplyViewModel
                        {
                            Content = r.Content,
                            ReplyDate = r.ReplyDate,
                            StudentName = r.Student?.UserName,
                            TeacherName = r.Teacher?.UserName
                        }).ToList() ?? new()
                    }).ToList()
                };

                return View(viewModel);
            }

            [HttpPost]
            public async Task<IActionResult> AddComment(int VideoId, string Content)
            {
                // يفضل ربط الطالب من الجلسة لاحقًا
                var comment = new Comment
                {
                    VideoId = VideoId,
                    StudentId = 1,
                    Content = Content,
                    CommentDate = DateTime.Now
                };
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { lessonId = _context.Videos.Find(VideoId)?.LessonId, videoId = VideoId });
            }

            [HttpPost]
            public async Task<IActionResult> AddReply(int CommentId, string Content)
            {
                var reply = new Reply
                {
                    CommentId = CommentId,
                    Content = Content,
                    ReplyDate = DateOnly.FromDateTime(DateTime.Now),
                    StudentId = 1 // أو من الجلسة
                };
                _context.Replys.Add(reply);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { videoId = _context.Comments.Find(CommentId)?.VideoId });
            }

            [HttpPost]
            public async Task<IActionResult> RateLesson(short LessonId, byte Rating)
            {
                var rating = new StudentRating
                {
                    LessonId = LessonId,
                    StudentId = 1, // من الجلسة
                    RatingValue = Rating
                };
                _context.StudentRatings.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { lessonId = LessonId });
            }
        }
    }


