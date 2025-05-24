using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models
{
    public abstract class Section : DisplayInfo
    {
        public int SectionId { get; set; }
        public enSectionType SectionType { get; set; }
    }
    public enum enSectionType
    {
        [Display(Name = "قسم دراسي")]
        StudySection,

        [Display(Name = "قسم المكتبة")]
        LibrarySection
    }
}
