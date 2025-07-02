using school_hub.Models;

namespace school_hub.Areas.Students.ViewModels
{
    public class StudentLessonViewModel
    {
        public short LessonId { get; set; }
        public int ExamId { get; set; }

        public List<Video> Videos { get; set; }
        public Video CurrentVideo { get; set; }
        public string CurrentVideoTitle => $"فيديو {CurrentVideo?.VideoId}";

        public List<CommentViewModel> Comments { get; set; }
    }

    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string StudentName { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public List<ReplyViewModel> Replies { get; set; }
    }

    public class ReplyViewModel
    {
        public string? StudentName { get; set; }
        public string? TeacherName { get; set; }
        public string Content { get; set; }
        public DateOnly ReplyDate { get; set; }
    }
}
