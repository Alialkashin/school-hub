using Microsoft.AspNetCore.Mvc.Rendering;
using school_hub.ViewModels;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputUnitViewModel : InputDisplayInfoViewModel
    {
        public short SubjectId { get; set; }
        public List<SelectListItem>? Subjects { get; set; }
    }
}
