using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models.Sections;
namespace school_hub.Models.Lesson
{
    public class Lesson
    {
        public short LessonId { get; set; }

        public short UnitId { get; set; }
        public byte LessonNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public short ExamId { get; set; }
        public int? PreviousLesson { get; set; }
        public Unit Unit { get; set; }
        public Exam.Exam Exam { get; set; }
        public ICollection<Video> Videos { get; set; }
        public ICollection<StudentRating> Ratings { get; set; }


    }
}
