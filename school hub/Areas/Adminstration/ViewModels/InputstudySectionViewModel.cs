using school_hub.Models;
using school_hub.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputstudySectionViewModel : InputDisplayInfoViewModel
    {
        public int SectionId { get; set; }
        public Section? Section { get; set; }
        [Display(Name = "الصورة الحالية")]
        public string? ExistingImagePath { get; set; }
    }
}
