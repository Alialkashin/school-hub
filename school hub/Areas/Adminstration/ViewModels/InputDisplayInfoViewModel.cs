using Microsoft.AspNetCore.Mvc.Rendering;
using school_hub.Models;
using System.ComponentModel.DataAnnotations;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputDisplayInfoViewModel
    {
       
      

  
            public string Name { get; set; }

            public string Description { get; set; }


            public IFormFile? File { get; set; }

 
        

    }
}
