using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace school_hub.Models
{
    public class Subject : DisplayInfo
    {
        public short SubjectId { get; set; }
        public int TotalDuration { get; set; }
        public int TeacherId { get; set; }
        public short StudyPlanId { get; set; }
        public ICollection<Unit> Units { get; set; }
        public Teacher Teacher { get; set; }//nav prop
        public StudyPlan StudyPlan { get; set; }
    }
}
