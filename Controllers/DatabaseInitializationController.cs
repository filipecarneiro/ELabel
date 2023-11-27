using ELabel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELabel.Controllers
{
    public class DatabaseInitializationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseInitializationController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> InitializeDatabase()
        {
            // Retrieve the admin password from the environment variable
            string? adminPassword = Environment.GetEnvironmentVariable("ELABEL_ADMIN_PASSWORD");

            if (adminPassword is null)
                adminPassword = "Admin";

            // Migrate database to latest version
            _context.Database.Migrate();

            // Ensure the necessary roles exist
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Check if the admin user already exists
            var adminUser = await _userManager.FindByNameAsync("Admin");
            if (adminUser == null)
            {
                // Create the admin user
                adminUser = new IdentityUser
                {
                    UserName = "Admin"
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    // Assign the Admin role to the admin user
                    await _userManager.AddToRoleAsync(adminUser, "Admin");

                    return Ok("Database initialized with admin user.");
                }

                return BadRequest("Failed to create the admin user.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(adminUser);
            var resultPasswordReset = await _userManager.ResetPasswordAsync(adminUser, token, adminPassword);

            if (resultPasswordReset.Succeeded)
            {
                return Ok("Admin user already exists. Password set.");
            }

            return BadRequest("Failed to set the admin user password.");
        }
    }

}
