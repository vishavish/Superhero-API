using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Superhero.Api.Entities;
using Superhero.Api.Entities.Auth;
using Superhero.Api.Models;

namespace Superhero.Api.Context
{
    public class AppDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AppDbInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task InitiailzeAsync()
        {
            await _context.Database.MigrateAsync();

            await SeedDataAsync();
        }
        
        public async Task SeedDataAsync()
        {
            await InitRoles();

            //create admin user
            var admin = new ApplicationUser { UserName = "admin", Email = "admin@localhost.com" };
            if(_userManager.Users.All(u => u.UserName != admin.UserName))
            {
                await _userManager.CreateAsync(admin, "Admin123*");
                await _userManager.AddToRoleAsync(admin, Roles.Admin);
            }

            await InitData();
        }

        public async Task InitRoles()
        {
            var userRole = new IdentityRole(Roles.User);
            var administratorRole = new IdentityRole(Roles.Admin);

            await _roleManager.CreateAsync(userRole);
            await _roleManager.CreateAsync(administratorRole);
        }

        public async Task InitData()
        {
            if(!_context.Heroes!.Any())
            {
                List<Hero> heroes = new()
                {
                    new Hero { HeroName = "Gagamboy", PowerLevel = 2, Superpower = "Spider" },
                    new Hero { HeroName = "Captain Barbell", PowerLevel = 4, Superpower = "Superhuman" },
                    new Hero { HeroName = "Joaquin Bordado", PowerLevel = 1, Superpower = "Delusional" }
                };

                _context.Heroes!.AddRange(heroes);
            }
           
            if(!_context.Organizations!.Any())
            {
                List<Organization> organizations = new()
                {
                    new Organization { Name = "Neo Heroes"},
                    new Organization { Name = "Hero Power" }
                };

                _context.Organizations!.AddRange(organizations);
            }

            await _context.SaveChangesAsync();
        }
    }
}
