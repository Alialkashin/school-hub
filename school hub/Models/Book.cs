using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models
{
    public class Book
    {

        [DisplayName("المعرف")]
        public short BookId { get; set; }

        [DisplayName("رقم القسم")]
        public int LibrarySectionId { get; set; }

        [DisplayName("عنوان الكتاب")]
        public string Title { get; set; }

        [DisplayName("الوصف")]
        public string Description { get; set; }

        [DisplayName(" الكتاب")]
        public string BookPath { get; set; }

        [DisplayName("القسم")]
        public LibrarySection LibrarySection { get; set; }



    }
}
