namespace school_hub.Models.Sections
{
    public class LibrarySection : Section
    {
        public ICollection<Book>? Books { get; set; }
    }
}
