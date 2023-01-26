using Microsoft.AspNetCore.Identity;
using System;
using TodoApp.Api.Db.Entities;

namespace TodoApp.Api.Seed
{
    public class Seeder
    {
        private readonly IServiceScope _scope;

        public Seeder(WebApplication app)
        {
            _scope = app.Services.CreateScope();
        }

        public async Task SeedRoles()
        {
            var roleManager = _scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();

            var roles = new string[] { "user", "admin" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var newRole = new RoleEntity() { Name = role };
                    await roleManager.CreateAsync(newRole);
                }

            }


        }

    }
}
