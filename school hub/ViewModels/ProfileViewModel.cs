using System.ComponentModel.DataAnnotations;

namespace school_hub.ViewModels
{
    public class ProfileViewModel
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Phone { get; set; }
        public string? ProfilePicturePath { get; set; }
        public bool IsActive { get; set; }
        public IFormFile? image { get; set; }

    }
}