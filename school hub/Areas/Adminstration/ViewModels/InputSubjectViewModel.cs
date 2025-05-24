using Microsoft.AspNetCore.Mvc.Rendering;
using school_hub.Models;
using System.ComponentModel;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputSubjectViewModel:InputDisplayInfoViewModel
    {
            public int TeacherId { get; set; }

            public short StudyPlanId { get; set; }
        public int TotalDuration { get; set; }
        public List<SelectListItem>? Teacher { get; set; }

            public List<SelectListItem>? StudyPlans { get; set; }


        }
    }


