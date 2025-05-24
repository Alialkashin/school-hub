using System.Collections;
using System.ComponentModel;
using school_hub.Models;
namespace school_hub.Models
{
    public class StudyPlan : DisplayInfo
    {

 
       
        [DisplayName("معرف الخطة")]
        public short StudyPlanId { get; set; }

        [DisplayName("معرف القسم الدراسي")]
        public int StudySectionId { get; set; }

        [DisplayName("القسم الدراسي")]
        public StudySection StudySection { get; set; }

        [DisplayName("طلاب الخطة الدراسية")]
        public ICollection<StudentStudyPlan> StudyPlanStudents { get; set; }

        [DisplayName("المواد")]
        public ICollection<Subject> Subjects { get; set; }
    }

}

