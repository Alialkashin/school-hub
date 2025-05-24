using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models;
namespace school_hub.Models
{
    public class StudentStudyPlan
    {
        [DisplayName("معرف الخطة")]
        public short PlanId { get; set; }

        [DisplayName("الخطة الدراسية")]
        public StudyPlan StudyPlan { get; set; }

        [DisplayName("معرف الطالب")]
        public int StudentId { get; set; }

        [DisplayName("الطالب")]
        public Student Student { get; set; }

        [DisplayName("حالة الدفع")]
        public enPaymentStatus PaymentStatus { get; set; }

    }
public enum enPaymentStatus
{
    Paid,
    Progress,
    Complete
}
}
