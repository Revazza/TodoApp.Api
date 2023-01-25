using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using TodoApp.Api.Db;
using TodoApp.Api.Db.Entities;

namespace TodoApp.Api.Auth
{
    public static class AuthConfigurator
    {

        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TodoAppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("TodoAppConnection"));
            });

            var issuer = builder.Configuration["JWT:Issuer"]!;
            var audience = builder.Configuration["JWT:Audience"]!;
            var key = builder.Configuration["JWT:SecretKey"]!;


            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidIssuer = issuer,
                ValidAudience = audience,

            };

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.SaveToken = false;
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("user",policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("user");
                });
                options.AddPolicy("admin", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("admin");
                });
            });


            builder.Services
                .AddIdentity<UserEntity, RoleEntity>(o =>
                {
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 1;
                    o.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<TodoAppDbContext>()
                .AddDefaultTokenProviders();


        }


    }
}
