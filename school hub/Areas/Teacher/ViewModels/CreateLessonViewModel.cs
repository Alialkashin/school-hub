using System.ComponentModel.DataAnnotations;

namespace school_hub.Areas.Teacher.ViewModels
{
   public class CreateLessonViewModel
{
    public int? LessonId { get; set; } // معرف الدرس (مطلوب للتعديل فقط)

    public short UnitId { get; set; }  // معرف الوحدة المرتبط بها الدرس

    public byte? LessonNo { get; set; } // رقم الدرس (للعرض فقط في التعديل)

    [Required(ErrorMessage = "عنوان الدرس مطلوب")]
    [StringLength(100, ErrorMessage = "عنوان الدرس يجب ألا يتجاوز 100 حرف")]
    public string Title { get; set; }
}

}