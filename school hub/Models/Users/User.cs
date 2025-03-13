using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using school_hub.Models.Lesson;

namespace school_hub.Models.Users
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? ProfilePicturePath { get; set; }
        public bool IsActive { get; set; }
        public enUserType UserType { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reply> Replys { get; set; }

    }
    public enum enUserType
    {
        Admin,
        Student,
        Teacher
    }

}
