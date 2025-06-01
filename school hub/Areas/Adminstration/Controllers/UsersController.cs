using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;
using school_hub.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace school_hub.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(AppDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Adminstration/Users
        public async Task<IActionResult> Index(string usertype, string isactive)
        {
            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(usertype) && Enum.TryParse<enUserType>(usertype, out var parsedUserType))
            {
                users = users.Where(u => u.UserType == parsedUserType);
            }

            if (!string.IsNullOrEmpty(isactive) && bool.TryParse(isactive, out var parsedIsActive))
            {
                users = users.Where(u => u.IsActive == parsedIsActive);
            }

            return View(await users.ToListAsync());
        }

        // GET: Adminstration/Users/Create
        public IActionResult Create()
        {
            var viewModel = new UserCreateViewModel
            {
                UserTypeList = GetUserTypeSelectList()
            };
            return View(viewModel);
        }

        // POST: Adminstration/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.UserTypeList = GetUserTypeSelectList();
                return View(model);
            }

            User newUser;

            switch (model.UserType)
            {
                case enUserType.Student:
                    newUser = new Student();
                    break;
                case enUserType.Teacher:
                    newUser = new Teacher();
                    break;
                case enUserType.Admin:
                default:
                    newUser = new Admin();
                    break;
            }

            newUser.Email = model.Email;
            newUser.UserName = model.Email;
            newUser.IsActive = model.IsActive;
            newUser.UserType = model.UserType;

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            model.UserTypeList = GetUserTypeSelectList();
            return View(model);
        }

        // GET: Adminstration/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();

            var viewModel = new UserEditViewModel
            {
                Id = user.Id,
                Email = user.Email,
                IsActive = user.IsActive,
                UserType = user.UserType,
                UserTypeList = GetUserTypeSelectList()
            };

            return View(viewModel);
        }

        // POST: Adminstration/Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.UserTypeList = GetUserTypeSelectList();
                return View(model);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
            if (user == null) return NotFound();

            user.Email = model.Email;
            user.IsActive = model.IsActive;
            user.UserType = model.UserType;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            model.UserTypeList = GetUserTypeSelectList();
            return View(model);
        }

        // GET: Adminstration/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: Adminstration/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }

        // Helper: Get enum as SelectList
        private List<SelectListItem> GetUserTypeSelectList()
        {
            return Enum.GetValues(typeof(enUserType))
                .Cast<enUserType>()
                .Select(x => new SelectListItem
                {
                    Value = ((int)x).ToString(),
                    Text = x.GetDisplayName()
                }).ToList();
        }
    }
}
