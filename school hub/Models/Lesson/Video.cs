using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models.Lesson
{
    public class Video
    {
        public int VideoId { get; set; }

        public short LessonId { get; set; }
        public string VideoPath { get; set; }
        public short Duration { get; set; }
        public Lesson Lesson { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<StudentView> Views { get; set; }
    }
}
