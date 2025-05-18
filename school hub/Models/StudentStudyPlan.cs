using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models.Sections;
using school_hub.Models.Users;
using school_hub.Models;
namespace school_hub.Models
{
    public class StudentStudyPlan
    {
        public short PlanId { get; set; }
        public StudyPlan StudyPlan { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public enPaymentStatus PaymentStatus { get; set; }


    }
}
public enum enPaymentStatus
{
    Paid,
    Progress
}
