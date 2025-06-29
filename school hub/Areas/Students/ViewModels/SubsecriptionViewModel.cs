using school_hub.Models;
namespace school_hub.Areas.Student.ViewModels
{
    public class SubsecriptionViewModel
    {
        public short studyPlanId { get; set; }
        public string StudyPlanName { get; set; }
        public string StudySectionName { get; set; }
        public enPaymentStatus paymentStatus { get; set; }
    }
}