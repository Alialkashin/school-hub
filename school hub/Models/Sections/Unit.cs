namespace school_hub.Models.Sections
{
    using school_hub.Models.Lesson;

    public class Unit : Section
    {

        public ICollection<Lesson> Lessons { get; set; }//navication ref
    }
}
