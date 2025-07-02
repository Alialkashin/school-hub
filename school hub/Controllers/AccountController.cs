using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Models;
using school_hub.ViewModels;

namespace school_hub.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly SignInManager<User> _signinManager;
        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signinManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Student student = new Student()
            {
                Email = model.Email,
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(student, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(student, enUserType.Student.ToString());

                await _signinManager.SignInAsync(student, false);
                Student? currentStudent = await _userManager.Users.OfType<Student>().FirstOrDefaultAsync(u => u.Email == model.Email);
                if (currentStudent != null)
                {
                    await _userManager.UpdateAsync(currentStudent);
                }
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();


        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _signinManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                     if (user.IsFirstLogin)
                     {
                        TempData["OldPassword"] = model.Password;
                         return RedirectToAction("ForceChangePassword");
                     }

                    return await RedirectByUserRoleAsync(user);
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signinManager.SignOutAsync();
            return Ok();
        }


        [Authorize]
        public IActionResult ForceChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ForceChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            var result = await _userManager.ChangePasswordAsync(user, TempData["OldPassword"].ToString(), model.NewPassword);

            if (result.Succeeded)
            {
                // حدث العلم
                user.IsFirstLogin = false;
                await _userManager.UpdateAsync(user);

                await _signinManager.RefreshSignInAsync(user); // تحديث تسجيل الدخول
                return await RedirectByUserRoleAsync(user);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        private async Task<LocalRedirectResult> RedirectByUserRoleAsync(User user)
        {
            
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains(enUserType.Admin.ToString()))
                return LocalRedirect("/Adminstration/StudySections/index");

            else if (roles.Contains(enUserType.Teacher.ToString()))
                return LocalRedirect("/Teacher/Subjects/index");

            else 
                return LocalRedirect("/Students/Subsecriptions/index");
                
        }


    }

}