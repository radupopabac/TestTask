using Identity.Core.Entities;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Identity.API.Di
{
    public static class RegisterDatabase
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                         options.UseSqlServer(
                             configuration.GetConnectionString("DefaultDb"),
                             x => x.MigrationsAssembly("Identity.API")));
            var build = services.BuildServiceProvider();

            // need to do data migrations before we set DataProtection
            var scope = build.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
           
            return services;
        }
        public static IServiceCollection AddAuthenticationRules(this IServiceCollection services)
        {
            services.AddDefaultIdentity<UserEntity>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
                config.Lockout.AllowedForNewUsers = true;
                config.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0);
                config.Lockout.MaxFailedAccessAttempts = 5;
                config.Password.RequireDigit = true;
                config.Password.RequiredLength = 8;
                config.Password.RequiredUniqueChars = 1;
                config.Password.RequireLowercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequireUppercase = true;
                config.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
