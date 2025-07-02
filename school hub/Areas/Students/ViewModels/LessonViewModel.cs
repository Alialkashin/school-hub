namespace school_hub.Areas.Students.ViewModels
{
    public class LessonViewModel
    {
        public short LessonId { get; set; }
        public byte LessonNo { get; set; }
        public string Title { get; set; }
        public double AverageRating { get; set; }
        public bool IsUnlocked { get; set; }
    }

}