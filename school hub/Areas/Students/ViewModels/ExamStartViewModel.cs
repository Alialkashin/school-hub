namespace school_hub.Areas.Students.ViewModels
{
    public class ExamStartViewModel
    {
        public short ExamId { get; set; }
        public string ExamTitle { get; set; }

        public List<ExamQuestionViewModel> Questions { get; set; }
    }

    public class ExamQuestionViewModel
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public List<AnswerOptionViewModel> Answers { get; set; }
    }

    public class AnswerOptionViewModel
    {
        public int AnswerId { get; set; }
        public string Text { get; set; }
    }
}
