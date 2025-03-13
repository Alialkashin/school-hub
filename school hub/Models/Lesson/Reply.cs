using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models.Users;

namespace school_hub.Models.Lesson
{
    public class Reply
    {
        public int ReplyId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateOnly ReplyDate { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}
