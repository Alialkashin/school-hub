using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models;

namespace school_hub.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int VideoId { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public ICollection<Reply>? Replys { get; set; }
        public Video? Video { get; set; }
        public Student? Student { get; set; }//notfacation prop
        public Teacher? Teacher { get; set; }

    }
}
