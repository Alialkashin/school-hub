using school_hub.Models;
namespace school_hub.Areas.Teacher.ViewModels
{
    public class StudyPlanSubjectsViewModel
    {
        public string StudyPlanName { get; set; }
        public List<Subject> Subjects { get; set; }
    }

}
