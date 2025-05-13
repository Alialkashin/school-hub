using System.Collections;
namespace school_hub.Models.Sections
{
    public class StudySection : Section
    {
        public ICollection<StudyPlan> StudyPlans { get; set; }
    }
}
