using school_hub.Models;

namespace school_hub.Areas.Adminstration.ViewModels
{
    public class InputStudyPlanRequestViewModel
    {
        public int RequestNumber { get; set; }
        public string StudentName { get; set; }
        public string StudyPlanName { get; set; }
        public int StudentId { get; set; }
        public short StudyPlanId { get;  set; }
        public enPaymentStatus PaymentStatus { get; set; }



    }


}
