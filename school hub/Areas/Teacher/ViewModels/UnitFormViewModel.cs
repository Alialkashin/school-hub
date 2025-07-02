using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
namespace school_hub.Areas.Teacher.ViewModels
{
    public class UnitFormViewModel
    {
        public short? UnitId { get; set; }

        [DisplayName("الاسم")]
        public string Name { get; set; }

        [DisplayName("الوصف")]
        public string Description { get; set; }

        [DisplayName("صورة الوحدة")]
        public IFormFile? ImageFile { get; set; }

        public string? ExistingImagePath { get; set; }

        [DisplayName("المادة")]
        public short SubjectId { get; set; }

        public List<SelectListItem> Subjects { get; set; } = new();
    }


}