using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models.Exam
{
    public class Exam
    {
        public short ExamId { get; set; }
        public short LessonId { get; set; }
        public byte PassingScore { get; set; }
        public TimeOnly ExamTime { get; set; }

        //public byte CountOfQuestion { get; set; }
        public Lesson.Lesson Lesson { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
    }

}
