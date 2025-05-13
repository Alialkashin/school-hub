using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using school_hub.Models.Lesson;

namespace school_hub.Models.Users
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
