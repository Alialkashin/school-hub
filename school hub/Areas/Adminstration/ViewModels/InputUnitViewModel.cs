using Microsoft.AspNetCore.Mvc.Rendering;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputUnitViewModel : InputDisplayInfoViewModel
    {
        public short SubjectId { get; set; }
        public List<SelectListItem>? Subjects { get; set; }
    }
}
