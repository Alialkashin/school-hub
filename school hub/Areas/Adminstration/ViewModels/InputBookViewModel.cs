using Microsoft.AspNetCore.Mvc.Rendering;
using school_hub.Models;
using System.ComponentModel.DataAnnotations;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputBookViewModel
    {





            public int Id { get; set; }

            [Required(ErrorMessage = "الرجاء إدخال اسم الكتاب")]
            [Display(Name = "اسم الكتاب")]
            public string Name { get; set; }

            [Display(Name = "الوصف")]
            public string Description { get; set; }

            [Display(Name = "صورة الكتاب")]
            public IFormFile? File { get; set; }

            public string? ExistingImagePath { get; set; }

            [Required(ErrorMessage = "الرجاء اختيار القسم")]
            [Display(Name = "القسم")]
            public int LibrarySectionId { get; set; }

            public List<SelectListItem> ?LibrarySectionItems { get; set; }
       

    //public int Id { get; set; }
    //public string Name { get; set; }
    //public string Description { get; set; }
    //public IFormFile File { get; set; }
    //public string? ExistingImagePath { get; set; }
    //public int LibrarySectionId { get; set; }

    //public List<SelectListItem>? LibrarySectionItems { get; set; }





}

}
