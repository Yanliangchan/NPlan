using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NPlan.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NPlan.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Seed roles
                string[] roleNames = { "Admin", "Member" };

                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new ApplicationRole(roleName));
                    }
                }

                // Seed admin user
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    Fullname = "Admin User",
                    StudentID = "Admin001",
                    VerificationStatus = "Verified",
                    Diploma = "AdminDiploma"
                };

                string adminPassword = "Admin@123";
                var user = await userManager.FindByEmailAsync(adminUser.Email);

                if (user == null)
                {
                    var createPowerUser = await userManager.CreateAsync(adminUser, adminPassword);
                    if (createPowerUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }

                // Seed interest groups
                if (!context.InterestGroups.Any())
                {
                    context.InterestGroups.AddRange(
                        new InterestGroup { Name = "Cybersecurity Club", Description = "Club focused on cybersecurity" },
                        new InterestGroup { Name = "Programming Club", Description = "Club for programming enthusiasts" }
                    );
                    await context.SaveChangesAsync();
                }

                // Seed events
                if (!context.Events.Any())
                {
                    var cybersecurityClub = context.InterestGroups.FirstOrDefault(g => g.Name == "Cybersecurity Club");
                    var programmingClub = context.InterestGroups.FirstOrDefault(g => g.Name == "Programming Club");

                    context.Events.AddRange(
                        new Event
                        {
                            EventName = "Cybersecurity Workshop",
                            Description = "Learn about the latest in cybersecurity.",
                            StartDateTime = DateTime.Now.AddDays(10),
                            EndDateTime = DateTime.Now.AddDays(10).AddHours(2),
                            Location = "Main Hall",
                            Points = 10,
                            InterestGroupId = cybersecurityClub.Id,
                            InterestGroup = cybersecurityClub
                        },
                        new Event
                        {
                            EventName = "Programming Bootcamp",
                            Description = "Intensive programming bootcamp.",
                            StartDateTime = DateTime.Now.AddDays(20),
                            EndDateTime = DateTime.Now.AddDays(20).AddHours(8),
                            Location = "Room 101",
                            Points = 20,
                            InterestGroupId = programmingClub.Id,
                            InterestGroup = programmingClub
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
