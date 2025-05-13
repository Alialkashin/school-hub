using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models.Lesson;
using school_hub.Models.Users;

namespace school_hub.Models
{
    public class StudentView
    {
        public int StudentId { get; set; }
        public int VideoId { get; set; }
        public bool IsComplete { get; set; }    
        public Student Student { get; set; }
        public Video Video { get; set; }
    }
}
