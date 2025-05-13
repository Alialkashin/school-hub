using school_hub.Models.Exam;
using school_hub.Models.Lesson;

namespace school_hub.Models.Users
{
    public class Student : User
    {
        
        public ICollection<StudentRating>? StudentRatings { get; set; }
        public ICollection<StudentView>? StudentViews { get; set; }
        public ICollection<StudentExam>? StudentExams { get; set; }
        public ICollection<StudentStudyPlan>? StudentStudyPlans { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Reply>? Replies { get; set; }
    }
}
