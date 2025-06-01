using Microsoft.AspNetCore.Identity;
using school_hub.Data;
using school_hub.Models;

namespace Data
{
    public static class RoleInitializer
    {
        public static async Task SeedRoleAsync(AppDBContext context, RoleManager<IdentityRole<int>> roleManager)
        {

            if (!context.Roles.Any())
            {

                string[] roleNames = { enUserType.Admin.ToString(), enUserType.Student.ToString(), enUserType.Teacher.ToString() };

                foreach (var roleName in roleNames)
                {
                    var roleExists = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                    }
                }
            }




        }



    }
}