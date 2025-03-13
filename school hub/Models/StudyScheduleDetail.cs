using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models.Sections;

namespace school_hub.Models
{
    public class StudyScheduleDetail
    {
        public byte ScheduleId { get; set; }
        public short SubjectId { get; set; }
        public DayOfWeek Day { get; set; }
        public Subject Subject { get; set; }
        public StudySchedule Schedule { get; set; }

    }
}

