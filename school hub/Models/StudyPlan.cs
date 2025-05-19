using System.Collections;
using school_hub.Models;
namespace school_hub.Models
{
    public class StudyPlan : DisplayInfo
    {

        public short StudyPlanId { get; set; }
        public short StudySectionId { get; set; }
        public StudySection StudySection { get; set; }
        public ICollection<StudentStudyPlan> StudyPlanStudents { get; set; } //nav references
        public ICollection<Subject> Subjects { get; set; }

    }
}
