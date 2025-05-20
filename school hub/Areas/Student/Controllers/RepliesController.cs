using Microsoft.AspNetCore.Mvc;
using school_hub.Data;
using school_hub.Models;

namespace school_hub.Areas.Student.Controllers
{
    public class RepliesController : Controller
    {
        private readonly AppDBContext _context;
        public RepliesController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult GetCommentReplies(int CommentId)
        {
            throw new NotImplementedException();
        }
        public IActionResult AddReply(Reply reply)
        {
            throw new NotImplementedException();
        }
        
    }
}