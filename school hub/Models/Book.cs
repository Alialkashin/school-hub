using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models
{
    public class Book
    {
        public short BookId { get; set; }
        public int LibrarySectionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BookPath { get; set; }
        public LibrarySection LibrarySection { get; set; }

    }
}
