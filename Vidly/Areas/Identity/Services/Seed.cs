using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vidly.Models;

namespace Vidly.Areas.Identity.Services
{
    public class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "User"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var admin = await UserManager.FindByEmailAsync("admin@vidly.com");
            if (admin == null)
            {
                admin = new ApplicationUser()
                {
                    UserName = "admin@vidly.com",
                    Email = "admin@vidly.com",
                };
                var adminUser = await UserManager.CreateAsync(admin, "Test@123");
                if (adminUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(admin, "Admin");
                }
            }

        }
    }
}
