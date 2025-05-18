using school_hub.Models;

namespace school_hub.Models
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
