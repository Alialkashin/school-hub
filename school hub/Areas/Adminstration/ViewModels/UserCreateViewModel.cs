using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using school_hub.Models;
public class UserCreateViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "كلمة المرور غير متطابقة")]
    public string ConfirmPassword { get; set; } = null!;

    [Required]
    public enUserType UserType { get; set; }

    public bool IsActive { get; set; } = true;

    public List<SelectListItem> UserTypeList { get; set; } = new();
}
