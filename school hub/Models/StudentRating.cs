using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace school_hub.Models
{
    public class StudentRating
    {
        public int StudentId { get; set; }
        public short LessonId { get; set; }

        public byte RatingValue { get; set; }
        public Student Student { get; set; }
        public Lesson Lesson { get; set; }

    }
}
