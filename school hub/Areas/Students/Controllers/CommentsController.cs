using Microsoft.AspNetCore.Mvc;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Students.Controllers
{
    public class CommentsController : Controller
    {
        private readonly AppDBContext _context;
        public CommentsController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult GetVideoComments(int VideoId)
        {
            throw new NotImplementedException();
        }
        public IActionResult GetStudentComments(int StudentId)
        {
            throw new NotImplementedException();
        }

        public IActionResult AddComment(Comment comment)
        {
            throw new NotImplementedException();
        }
        
    }
}