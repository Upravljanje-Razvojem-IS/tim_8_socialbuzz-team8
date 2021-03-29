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
                ApplicationUser adminUser = new ApplicationUser("admin", "admin@gmail.com", "+38105056665", UserAccountTypesExtensions.Admin);

                IdentityResult result = userManager.CreateAsync(adminUser).Result;

                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Administrator").Wait();
                }
            }

            if(userManager.FindByNameAsync("fefolino").Result == null)
            {
                ApplicationUser personalUser = new ApplicationUser("fefolino", "fefa@gmail.com", "+38105050505", UserAccountTypesExtensions.Personal);

                IdentityResult result = userManager.CreateAsync(personalUser).Result;

                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(personalUser, "RegularUser").Wait();
                }
            }

            if (userManager.FindByNameAsync("dexico").Result == null)
            {
                ApplicationUser corporatelUser = new ApplicationUser("dexico", "dexico@gmail.com", "+3810505025", UserAccountTypesExtensions.Corporate);

                IdentityResult result = userManager.CreateAsync(corporatelUser).Result;

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
                ApplicationRole adminRole = new ApplicationRole("Role that enables root level privileges", "RegularUser");
                IdentityResult roleResult = roleManager.CreateAsync(adminRole).Result;
            }

            if (!roleManager.RoleExistsAsync("RegularUser").Result)
            {
                ApplicationRole regularUserRole = new ApplicationRole("Role that enables basic level privileges", "RegularUser");
                IdentityResult roleResult = roleManager.CreateAsync(regularUserRole).Result;
            }
        }

    }
}
