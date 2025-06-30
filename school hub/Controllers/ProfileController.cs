using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using school_hub.Data;
using school_hub.Models;
using school_hub.ViewModels;

namespace school_hub.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        private readonly AppDBContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        public ProfileController(AppDBContext context, IWebHostEnvironment env, UserManager<User> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            User? user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ProfileViewModel model = new ProfileViewModel()
            {
                Email = user.Email,
                UserName = user.UserName,
                Phone = user.PhoneNumber,
                ProfilePicturePath = user.ProfilePicturePath
            };
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MyProfile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = await _userManager.FindByIdAsync(UserId.ToString());
            if (model.image != null && model.image.Length > 0)
            {
                string uploadFolder = Path.Combine(_env.WebRootPath, "images", "ProfilePictures");
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.image.FileName);
                string filepath = Path.Combine(uploadFolder, filename);
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await model.image.CopyToAsync(stream);
                }
                user.ProfilePicturePath = filepath;

                if (!string.IsNullOrEmpty(model.ProfilePicturePath))
                {
                    string oldImage = Path.Combine(_env.WebRootPath, model.ProfilePicturePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }

            }
            user.PhoneNumber = model.Phone;
            user.UserName = model.UserName;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            return RedirectToAction("MyProfile");
        }
    }
}