
using System.ComponentModel;

namespace school_hub.Models
{

    public class Unit : DisplayInfo
    {
        [DisplayName("معرف الوحدة")]
        public short UnitId { get; set; }

        [DisplayName("الدروس")]
        public ICollection<Lesson> Lessons { get; set; }

        [DisplayName("معرف المادة")]
        public short SubjectId { get; set; }

        [DisplayName("المادة")]
        public Subject Subject { get; set; }
    }

}

