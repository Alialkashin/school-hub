using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace school_hub.ViewModels
{
    public class InputDisplayInfoViewModel
    {
        [DisplayName("الاسم")]
            public string Name { get; set; }
        [DisplayName("الوصف")]
        public string Description { get; set; }
        [DisplayName("الصورة")]
        public IFormFile? File { get; set; }
    }

}