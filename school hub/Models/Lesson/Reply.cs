using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models;

namespace school_hub.Models
{
    public class Reply
    {
        public int ReplyId { get; set; }
        public int CommentId { get; set; }
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }
        public required string Content { get; set; }
        public DateOnly ReplyDate { get; set; }
        public Comment? Comment { get; set; }
        public Teacher? Teacher { get; set; }
        public Student? Student { get; set; }
    }
}
