using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace eLab.Data
{
    public class SeedData
    {
        private static async Task RoleCreation(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(UserRole.Admin))
            {
                var adminRole = new IdentityRole() { Name = UserRole.Admin, NormalizedName = UserRole.Admin.ToUpper() };
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync(UserRole.User))
            {
                var userRole = new IdentityRole() { Name = UserRole.User, NormalizedName = UserRole.User.ToUpper() };
                await roleManager.CreateAsync(userRole);
            }
        }

        private static async Task UserCreation(UserManager<ApplicationUser> userManager)
        {
            var admin = new ApplicationUser()
            {
                UserName = "admin@medifox.com",
                NormalizedUserName = "ADMIN@MEDIFOX.COM",
                Email = "admin@medifox.com",
                NormalizedEmail = "ADMIN@MEDIFOX.COM",
                EmailConfirmed = true,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            var user = new ApplicationUser()
            {
                UserName = "prabhat@medifox.com",
                NormalizedUserName = "PRABHAT@MEDIFOX.COM",
                Email = "prabhat@medifox.com",
                NormalizedEmail = "PRABHAT@MEDIFOX.COM",
                EmailConfirmed = true,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            if (userManager.FindByNameAsync(admin.UserName).Result == null)
            {
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, UserRole.Admin);
            }

            if (userManager.FindByNameAsync(user.UserName).Result == null)
            {
                await userManager.CreateAsync(user, "User@123");
                await userManager.AddToRoleAsync(user, UserRole.User);
            }
        }

        public static async Task DatabaseSeedingAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await RoleCreation(roleManager);
            await UserCreation(userManager);
        }
    }
}
