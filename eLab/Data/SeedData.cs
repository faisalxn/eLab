using eLab.Models;
using Microsoft.AspNetCore.Identity;

namespace eLab.Data
{
    public class SeedData
    {
        public static async Task RoleCreation(RoleManager<IdentityRole> roleManager)
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

        public static async Task UserCreation(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();


            var admin = new ApplicationUser()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@medifox.com",
                NormalizedEmail = "ADMIN@MEDIFOX.COM",
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin@123");


            var user = new ApplicationUser()
            {
                UserName = "prabhat",
                NormalizedUserName = "PRABHAT",
                Email = "prabhat@medifox.com",
                NormalizedEmail = "PRABHAT@MEDIFOX.COM",
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "User@123");


            if(userManager.FindByNameAsync(admin.UserName).Result == null)
            {
                await userManager.CreateAsync(admin);
                await userManager.AddToRoleAsync(admin, UserRole.Admin);
            }
            
            if(userManager.FindByNameAsync(user.UserName).Result == null)
            {
                await userManager.CreateAsync(user);
                await userManager.AddToRoleAsync(user, UserRole.User);
            }
        }

        public static async Task DatabaseSeedingAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await RoleCreation(roleManager);
            await UserCreation(context, userManager);
        }
    }
}
