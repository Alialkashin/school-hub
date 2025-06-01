using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using school_hub.Models;
public class UserEditViewModel
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public enUserType UserType { get; set; }

    public bool IsActive { get; set; }

    public List<SelectListItem> UserTypeList { get; set; } = new();
}
