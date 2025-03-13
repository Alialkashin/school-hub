using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models.Exam
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public Question Question { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
