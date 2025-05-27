using school_hub.Models;

namespace school_hub.Models
{
    public class Teacher : User
    {
        public Subject? Subject { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Reply>? Replies { get; set; }
    }
}
