namespace school_hub.Models
{
    public class LibrarySection : Section
    {
        public ICollection<Book>? Books { get; set; }
    }
}
