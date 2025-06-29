using Microsoft.AspNetCore.Mvc.Rendering;
using school_hub.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using school_hub.ViewModels;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputSubjectViewModel : InputDisplayInfoViewModel
    {
        public short SubjectId { get; set; }
        [Display(Name = "المعلم المسؤول عن المادة")]
        public int TeacherId { get; set; }
        [Display(Name = "الخطة الدراسية")]
        public short StudyPlanId { get; set; }
        [Display(Name = "المدة التقريبية للمادة")]
        public int TotalDuration { get; set; }
        public List<SelectListItem>? Teachers { get; set; }

        public List<SelectListItem>? StudyPlans { get; set; }
        public string? ExistingImagePath { get; set; }


    }
    
}


