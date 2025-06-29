using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models
{
    public class Book
    {

       
        public short BookId { get; set; }

        [DisplayName(" القسم")]
        public int LibrarySectionId { get; set; }

        [DisplayName("عنوان الكتاب")]
        public string Title { get; set; }

        [DisplayName("الوصف")]
        public string Description { get; set; }

        [DisplayName("PDF الكتاب")]
        public string BookPath { get; set; }

      
        [DisplayName("عدد الصفحات")]
        public int PageCount { get; set; }

        [DisplayName("تاريخ الرفع")]
        public DateTime UploadDate { get; set; }
        [DisplayName("القسم")]
        public LibrarySection LibrarySection { get; set; }
     



    }
}
