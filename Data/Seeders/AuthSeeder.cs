using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Data.Contexts;

namespace UniWall.Data.Seeders
{
    public class AuthSeeder : ISeeder
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthDbContext _dbContext;
        public AuthSeeder(IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            DbContextOptions<AuthDbContext> dbOptions = serviceProvider.GetRequiredService<DbContextOptions<AuthDbContext>>();

            _dbContext = new AuthDbContext(dbOptions);
            _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        }

        public async Task Seed()
        {
            bool roleSeeded = true;

            if (!_dbContext.Roles.Any())
            {
                roleSeeded = await SeedRoles();
            }
            if (roleSeeded && !_dbContext.Users.Any())
            {
                await SeedUsers();
            }
        }

        private async Task<bool> SeedRoles()
        {

            IdentityRole[] roles = new IdentityRole[]
            {
                new() { Name = "api.admin", NormalizedName = "API.ADMIN" },
                new() { Name = "post.reader", NormalizedName = "POST.READER" },
                new() { Name = "post.writer", NormalizedName = "POST.WRITER" },
                new() { Name = "user.manager", NormalizedName = "USER.MANAGER" },
                new() { Name = "post.moderator", NormalizedName = "POST.MODERATOR" },
            };

            foreach(IdentityRole role in roles)
            {
                await _roleManager.CreateAsync(role);
            }

            return true;

        }

        private async Task<bool> SeedUsers()
        {
            IdentityUser admin = new IdentityUser();
            admin.Email = "admin@univ.pl";
            admin.UserName = "admion";
            admin.NormalizedEmail = "ADMIN@UNIV.PL";
            admin.NormalizedUserName = "ADMION";
            admin.PasswordHash = _userManager.PasswordHasher.HashPassword(admin, "zaq1@WSX");

            await _userManager.CreateAsync(admin);
            await _userManager.AddToRoleAsync(admin, "api.admin");
            await _userManager.AddToRoleAsync(admin, "post.reader");
            await _userManager.AddToRoleAsync(admin, "post.writer");
            await _userManager.AddToRoleAsync(admin, "user.manager");
            await _userManager.AddToRoleAsync(admin, "post.moderator");

            return true;
        }
    }
}
