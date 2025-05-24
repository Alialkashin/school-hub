using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace school_hub.Models
{
    public class Subject : DisplayInfo
    {
     
     
        public short SubjectId { get; set; }

        [DisplayName("المدة الكاملة")]
        public int TotalDuration { get; set; }

        [DisplayName("معرف المعلم")]
        public int TeacherId { get; set; }

        [DisplayName("معرف الخطة الدراسية")]
        public short StudyPlanId { get; set; }

        [DisplayName("الوحدات")]
        public ICollection<Unit> Units { get; set; }

        [DisplayName("المعلم")]
        public Teacher Teacher { get; set; }

        [DisplayName("الخطة الدراسية")]
        public StudyPlan StudyPlan { get; set; }
    

}
}
