using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models.Exam
{
    public class StudentAnswer
    {
        public int StudentExamId { get; set; }
        public int AnswerId { get; set; }
        public StudentExam StudentExam { get; set; }
        public Answer Answer { get; set; }

    }
}
