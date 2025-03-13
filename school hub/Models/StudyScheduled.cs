using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models.Sections;

namespace school_hub.Models
{
    public class StudySchedule
    {
        public byte ScheduleId { get; set; }
        public short PlanId { get; set; }
        public StudyPlan StudyPlan { get; set; }
        public ICollection<LessonAvailableInSchedule> LessonAvailableInSchedules { get; set; }
        public ICollection<StudyScheduleDetail> StudyScheduleDetails { get; set; }

    }
}
