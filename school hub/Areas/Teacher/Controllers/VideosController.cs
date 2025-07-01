using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Areas.Teacher.ViewModels;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Teacher.Controllers
{
    [Route("Videos")]
    [Area("Teacher")]
public class VideosController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;
        public VideosController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet("GetByLesson/{lessonId}")]
        public async Task<IActionResult> GetByLesson(short lessonId)
        {
            var videos = await _context.Videos
                .Where(v => v.LessonId == lessonId)
                .Select(v => new
                {
                    v.VideoId,
                    v.VideoPath,
                    v.Duration
                })
                .ToListAsync();

            return Json(videos);
        }

        [HttpGet("Manage/{lessonId}")]
        public IActionResult Manage(short lessonId)
        {
            ManageLessonVideos model = new ManageLessonVideos();
            model.LessonId = lessonId;
            model.UnitId = _context.Lessons.Where(l => l.LessonId == lessonId).Select(l => l.UnitId).FirstOrDefault();
            return View("Index", model);
        }


        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null) return NotFound();

            return Json(new
            {
                video.VideoId,
                video.LessonId,
                video.VideoPath,
                video.Duration
            });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(IFormFile videoFile, short lessonId, short duration)
        {
            if (videoFile == null || videoFile.Length == 0)
                return BadRequest("يرجى تحديد ملف فيديو.");


            var maxId = await _context.Videos
                .Where(v => v.LessonId == lessonId)
                .Select(v => (int?)v.VideoId)
                .MaxAsync();

            var video = new Video
            {
                LessonId = lessonId,
                Duration = duration,
                PreviousVideo = maxId
            };
            video.VideoPath = await SaveVideoAsync(videoFile, video.VideoPath);

            await _context.Videos.AddAsync(video);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(IFormFile? videoFile, int videoId, short duration)
        {
            var video = await _context.Videos.FindAsync(videoId);
            if (video == null)
                return NotFound();

            // إن تم إرسال فيديو جديد
            video.VideoPath = await SaveVideoAsync(videoFile, video.VideoPath);


            video.Duration = duration;
            _context.Videos.Update(video);
            await _context.SaveChangesAsync();
            return Ok();
        }
        public async Task<string> SaveVideoAsync(IFormFile? videoFile, string oldFile)
        {
            if (videoFile == null || videoFile.Length == 0)
            {
                return oldFile;
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath, "videos");
            Directory.CreateDirectory(uploadsFolder); // تأكد من وجود المجلد

            var uniqueFilename = Guid.NewGuid().ToString() + Path.GetExtension(videoFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFilename);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await videoFile.CopyToAsync(fileStream);
            }

            // حذف الملف القديم إن وجد
            if (!string.IsNullOrEmpty(oldFile))
            {
                var oldFilePath = Path.Combine(_env.WebRootPath, oldFile.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            return "/videos/" + uniqueFilename;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null) return NotFound();

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}