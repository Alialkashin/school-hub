using System.ComponentModel;

namespace school_hub.Models
{
    public class DisplayInfo //DisplayInfo
    {
      
        [DisplayName("الاسم")]
        public string Name { get; set; }

        [DisplayName("الوصف")]
        public string Description { get; set; }

        [DisplayName("رابط الصورة")]
        public string ImagePath { get; set; }

    }

}
