using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputStudyPlansViewModel:InputDisplayInfoViewModel
    {
        public short Id { get; set; }
        [Display(Name = "القسم الدراسي")]
        public int StudySectionId { get; set; }
        public List<SelectListItem>? StudySectionItems { get; set; }

        public string? ExistingImagePath { get; set; }
    }
}
