using Microsoft.AspNetCore.Mvc.Rendering;
using school_hub.ViewModels;
namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputStudyPlansViewModel:InputDisplayInfoViewModel
    {
        public int StudySectionId { get; set; }
        public List<SelectListItem>? StudySection { get; set; }
   
    }
}
