using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace school_hub.Models
{
    public abstract class User : IdentityUser<int>
    {
        public string? ProfilePicturePath { get; set; }
        public bool IsActive { get; set; }
        public enUserType UserType { get; set; }
    }
    public enum enUserType
    {
        Admin,
        Student,
        Teacher
    }

}
