using System.ComponentModel.DataAnnotations;

namespace school_hub.ViewModels
{
    public class ChangePasswordViewModel
{

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "كلمة المرور الجديدة")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "تأكيد كلمة المرور")]
    [Compare("NewPassword", ErrorMessage = "كلمة المرور غير متطابقة.")]
    public string ConfirmPassword { get; set; }
}

}