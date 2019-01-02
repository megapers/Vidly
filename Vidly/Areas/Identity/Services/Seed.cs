using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vidly.Areas.Identity.Services
{
    public class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "User", "HR", "CanManageMovies" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser user = await UserManager.FindByEmailAsync("admin@gmail.com");
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                };
                await UserManager.CreateAsync(user, "Test@123");
            }
            await UserManager.AddToRoleAsync(user, "Admin");


            IdentityUser user1 = await UserManager.FindByEmailAsync("user@gmail.com");
            if (user1 == null)
            {
                user1 = new IdentityUser()
                {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                };
                await UserManager.CreateAsync(user1, "Test@123");
            }
            await UserManager.AddToRoleAsync(user1, "User");

            IdentityUser user2 = await UserManager.FindByEmailAsync("hr@gmail.com");
            if (user2 == null)
            {
                user2 = new IdentityUser()
                {
                    UserName = "hr@gmail.com",
                    Email = "hr@gmail.com",
                };
                await UserManager.CreateAsync(user2, "Test@123");
            }
            await UserManager.AddToRoleAsync(user2, "HR");

            IdentityUser user3 = await UserManager.FindByEmailAsync("admin@vidly.com");
            if (user3 == null)
            {
                user3 = new IdentityUser()
                {
                    UserName = "admin@vidly.com",
                    Email = "admin@vidly.com",
                };
                await UserManager.CreateAsync(user3, "2783Test!");
            }
            await UserManager.AddToRoleAsync(user3, "CanManageMovies");

        }
    }
}
