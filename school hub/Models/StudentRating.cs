using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models.Users;
using school_hub.Models.Lesson;
namespace school_hub.Models
{
    public class StudentRating
    {
        public int StudentId { get; set; }
        public short LessonId { get; set; }

        public byte RatingValue { get; set; }
        public Student Student { get; set; }
        public Lesson.Lesson Lesson { get; set; }

    }
}
