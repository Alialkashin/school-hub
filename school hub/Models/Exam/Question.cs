using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models.Exam
{
    public class Question
    {
        public int QuestionId { get; set; }

        public short ExamId { get; set; }
        public string Text { get; set; }
        public Exam Exam { get; set; }
        public ICollection<Answer> Answers { get; set; }

    }
}
