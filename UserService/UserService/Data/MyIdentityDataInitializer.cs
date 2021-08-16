using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Entities;
using UserService.Models;

namespace UserService.Data
{
    public class MyIdentityDataInitializer
    {
        public static void SeedUsersAndRoles (UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);

            SeedUsers(userManager);
        }

        public static void SeedUsers (UserManager<ApplicationUser> userManager)
        {
            if(userManager.FindByNameAsync("admin").Result == null)
            {
                ApplicationUser adminUser = new ApplicationUser();
                adminUser.Email = "admin@gmail.com";
                adminUser.UserName = "admin";

                IdentityResult result = userManager.CreateAsync(adminUser, "Admin98.").Result;

                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Administrator").Wait();
                }
            }

            if(userManager.FindByNameAsync("fefolino").Result == null)
            {
                ApplicationUser personalUser = new ApplicationUser();
                personalUser.Email = "pers@gmail.com";
                personalUser.UserName = "Pers";

                IdentityResult result = userManager.CreateAsync(personalUser, "Fefa98.").Result;

                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(personalUser, "RegularUser").Wait();
                }
            }

            if (userManager.FindByNameAsync("dexico").Result == null)
            {
                ApplicationUser corporatelUser = new ApplicationUser();
                corporatelUser.Email = "corp@gmail.com";
                corporatelUser.UserName = "Corp";

                IdentityResult result = userManager.CreateAsync(corporatelUser, "Corporate98.").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(corporatelUser, "RegularUser").Wait();
                }
            }

        }

        public static void SeedRoles (RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync ("Administrator").Result)
            {
                ApplicationRole adminRole = new ApplicationRole();
                adminRole.Name = "Administrator";
                adminRole.Description = "Enables root level privileges";
                IdentityResult roleResult = roleManager.CreateAsync(adminRole).Result;
            }

            if (!roleManager.RoleExistsAsync("RegularUser").Result)
            {
                ApplicationRole regularUserRole = new ApplicationRole();
                regularUserRole.Name = "RegularUser";
                regularUserRole.Description = "Enables basic level privileges";
                IdentityResult roleResult = roleManager.CreateAsync(regularUserRole).Result;
            }
        }

    }
}
