using Microsoft.AspNetCore.Mvc.Rendering;
using school_hub.Models;
using System.ComponentModel.DataAnnotations;
using school_hub.ViewModels;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputLibraryViewModel:InputDisplayInfoViewModel
    {
        public int SectionId { get; set; }
        public Section? Section { get; set; }
        [Display(Name = "الصورة الحالية")]
        public string? ExistingImagePath { get; set; }
    }
}
