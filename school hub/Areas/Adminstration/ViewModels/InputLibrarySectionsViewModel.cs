using Microsoft.AspNetCore.Mvc.Rendering;
using school_hub.Models;
using System.ComponentModel.DataAnnotations;
using school_hub.ViewModels;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputLibrarySectionViewModel : InputDisplayInfoViewModel
    {
        public List<InputBookViewModel>? Books { get; set; }
    
    }

}
