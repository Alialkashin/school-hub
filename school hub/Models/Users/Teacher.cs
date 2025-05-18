using school_hub.Models;

namespace school_hub.Models
{
    public class Teacher : User
    {
        public ICollection<Subject>? Subjects { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Reply>? Replies { get; set; }
    }
}
