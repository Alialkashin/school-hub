using Microsoft.AspNetCore.Mvc.Rendering;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputStudyPlansViewModel:InputDisplayInfoViewModel
    {
        public int StudySectionId { get; set; }
        public List<SelectListItem>? StudySection { get; set; }
   
    }
}
