using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shopping.Server.Configuration;
using Shopping.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager, List<string> userRoleNames, ILogger logger = null)
        {
            if (userRoleNames == null) 
            {
                logger?.LogWarning("No user roles found");
            }
            else
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
        }
        public static void SeedUsers(UserManager<ShoppingUser> userManager, AdminSettings settings, List<string> userRoleNames, ILogger logger = null)
        {
            if (settings == null)
            {
                logger?.LogWarning("No admin settings found");
            }
            else if (userRoleNames == null)
            {
                logger?.LogWarning("No user roles found");
            }
            else
            {
                ShoppingUser admin = userManager.FindByEmailAsync(settings.Email).Result;

                if (admin == null)
                {
                    admin = new ShoppingUser
                    {
                        UserName = settings.UserName,
                        Email = settings.Email
                    };
                    var result = userManager.CreateAsync(admin, settings.Password).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Could not create admin user");
                    }
                }

                foreach (var role in userRoleNames)
                {
                    if (!userManager.IsInRoleAsync(admin, role).Result)
                    {
                        var result = userManager.AddToRoleAsync(admin, role).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception($"Could not add admin user to role: {role}");
                        }
                    }
                }
            }
        }
    }
}
