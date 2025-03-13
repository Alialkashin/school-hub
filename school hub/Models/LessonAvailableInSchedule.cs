using school_hub.Models.Lesson;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models
{
    public class LessonAvailableInSchedule
    {
        public short LessonId { get; set; }

        public byte ScheduleId { get; set; }
        public Lesson.Lesson Lesson { get; set; }
        public StudySchedule Schedule { get; set; }

    }
}
