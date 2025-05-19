
namespace school_hub.Models
{

    public class Unit : DisplayInfo
    {
        public short UnitId { get; set; }
        public ICollection<Lesson> Lessons { get; set; }//navication ref
        public short SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
