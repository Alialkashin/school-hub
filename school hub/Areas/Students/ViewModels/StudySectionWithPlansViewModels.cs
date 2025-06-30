namespace school_hub.Areas.Student.ViewModels
{
    public class StudySectionWithPlansViewModel
    {
        public string StudySectionName { get; set; } // اسم القسم
        public List<SubsecriptionViewModel> StudyPlans { get; set; } // الخطط التابعة للقسم
    }

}