using school_hub.Models;

namespace school_hub.ViewModles
{
    public class SubsecriptionViewModel
    {
        public short studyPlanId { get; set; }
        public StudyPlan studyPlan { get; set; }
        public enPaymentStatus paymentStatus { get; set; }
    }
}