using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskMangmentSystem.API.Helpers;
using TaskMangmentSystem.Core.Entities;
using TaskMangmentSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskMangmentSystem.API.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            var builder = services.AddIdentityCore<Employee>();

            // Map JWT Values
            builder.Services.Configure<JWT>(config.GetSection("JWT"));

            // Use Identity
            services.AddIdentity<Employee, IdentityRole>()
            .AddEntityFrameworkStores<IssueContext>()
            .AddDefaultTokenProviders();
            // Auth Service
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);

            builder.AddSignInManager<SignInManager<Employee>>();


            // Auth Schema
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = config["JWT:Issuer"],
                    ValidAudience = config["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config["JWT:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
