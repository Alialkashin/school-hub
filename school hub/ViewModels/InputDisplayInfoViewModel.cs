using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace school_hub.ViewModels
{
    public class InputDisplayInfoViewModel
    {
        [DisplayName("«·«”„")]
            public string Name { get; set; }
        [DisplayName("«·Ê’›")]
        public string Description { get; set; }
        [DisplayName("«·’Ê—…")]
        public IFormFile? File { get; set; }
    }

}