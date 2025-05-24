using Microsoft.AspNetCore.Mvc.Rendering;
using school_hub.Models;
using System.ComponentModel.DataAnnotations;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputBookViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? File { get; set; }
        public int LibrarySectionId { get; set; }

        public List<SelectListItem>? LibrarySection { get; set; }

     


        public List<SelectListItem> Sections { get; set; }
        public List<SelectListItem> SectionType { get; set; }
    
    }

}
