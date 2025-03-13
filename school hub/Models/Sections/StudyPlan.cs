namespace school_hub.Models.Sections
{
    public class StudyPlan : Section
    {


        public StudySchedule StudySchedule { get; set; }//nav prop
        public ICollection<StudentStudyPlan> StudyPlanStudents { get; set; } //nav references

    }
}
