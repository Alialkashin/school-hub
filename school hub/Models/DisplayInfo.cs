using System.ComponentModel;

namespace school_hub.Models
{
    public class DisplayInfo //DisplayInfo
    {
      
        [DisplayName("�����")]
        public string Name { get; set; }

        [DisplayName("�����")]
        public string Description { get; set; }

        [DisplayName("���� ������")]
        public string ImagePath { get; set; }

    }

}
