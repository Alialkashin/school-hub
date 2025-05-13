using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models.Users;

namespace school_hub.Models.Lesson
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int VideoId { get; set; }
        public string Content { get; set; }
        public DateOnly CommentDate { get; set; }
        public ICollection<Reply>? Replys { get; set; }
        public Video? Video { get; set; }
        public Student? Student { get; set; }//notfacation prop
        public Teacher? Teacher { get; set; }

    }
}
