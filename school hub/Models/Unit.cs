
namespace school_hub.Models
{
    using school_hub.Models.Lesson;

    public class Unit : DisplayInfo
    {
        public short UnitId { get; set; }
        public ICollection<Lesson.Lesson> Lessons { get; set; }//navication ref
        public short SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
