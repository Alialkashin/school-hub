using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models
{
    public abstract class Section : DisplayInfo
    {
        public short SectionId { get; set; }
        public enSectionType SectionType { get; set; }
    }
    public enum enSectionType
    {
        StudySection,
        LibrarySection
    }
}
