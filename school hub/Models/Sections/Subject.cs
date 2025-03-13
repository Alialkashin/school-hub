using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using school_hub.Models.Users;

namespace school_hub.Models.Sections
{
    public class Subject : Section
    {

        public int TeacherId { get; set; }
        public int TotalDuration { get; set; }
        public Teacher Teacher { get; set; }//nav prop
        public ICollection<StudyScheduleDetail> ScheduleDetails { get; set; }
    }
}
