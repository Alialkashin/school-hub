using school_hub.Models.Lesson;
using school_hub.Models.Sections;

namespace school_hub.Models.Users
{
    public class Teacher : User
    {
        public ICollection<Subject> Subjects { get; set; }
    }
}
