using school_hub.Models;

namespace school_hub.ViewModels
{
    public class StudySectionDetailsViewModel
    {
        public StudySection Section { get; set; }
        public int? StudentId { get; set; }
        public List<short> SubscribedPlanIds { get; set; } = new List<short>();
    }
}
