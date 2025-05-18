using System.Collections;
namespace school_hub.Models
{
    public class StudySection : Section
    {
        public ICollection<StudyPlan> StudyPlans { get; set; }
    }
}
