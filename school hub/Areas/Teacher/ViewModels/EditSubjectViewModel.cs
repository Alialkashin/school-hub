using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace school_hub.Areas.Teacher.ViewModels
{
    public class EditSubjectViewModel
    {
        public short SubjectId { get; set; }

        [DisplayName("اسم المادة")]
        public string Name { get; set; }

        [DisplayName("المدة الكاملة")]
        [Range(1, 500)]
        public int TotalDuration { get; set; }

        [DisplayName("الصورة الحالية")]
        public string? ExistingImagePath { get; set; }

        [DisplayName("صورة جديدة")]
        public IFormFile? NewImage { get; set; }
    }

}