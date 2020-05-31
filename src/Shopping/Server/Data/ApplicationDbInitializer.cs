using Microsoft.AspNetCore.Identity;
using Shopping.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Data
{
    public static class ApplicationDbInitializer
    {
        private static List<string> userRoleNames = new List<string>
        {
            "Admin", "Manager", "Creator", "User"
        };
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in userRoleNames)
            {
                if (roleManager.FindByNameAsync(roleName).Result == null)
                {
                    var result = roleManager.CreateAsync(new IdentityRole
                    {
                        Name = roleName
                    }).Result;
                }
            }
        }
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@shopping.de").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@shopping.de"
                };
                var result = userManager.CreateAsync(user, "Shopping3105!#2020").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
