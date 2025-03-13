using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models.Sections
{
    public abstract class Section
    {
        public short SectionId { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public short? ParentId { get; set; }
        public virtual Section? Parent { get; set; }
        public ICollection<Section> Sections { get; set; }
        public enSectionType SectionType { get; set; }

    }
    public enum enSectionType
    {
        Unit,
        Subject,
        StudySection,
        StudyPlan,
        LibrarySection
    }
}
