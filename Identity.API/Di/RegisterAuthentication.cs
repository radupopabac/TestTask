using Identity.Core.AppSettings;
using Identity.Core.Interfaces;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Identity.API.Di
{
    public static class RegisterAuthentication
    {
        public static IServiceCollection AddUserAuthentication(this IServiceCollection services)
        {
            services.AddScoped<ITokenValidation, TokenValidation>();
            var builder = services.BuildServiceProvider();
            var appSettings = builder.GetService<IOptions<AppSettings>>().Value;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = true;
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async ctx =>
                        {
                            await ctx.HttpContext.RequestServices.GetRequiredService<ITokenValidation>()
                               .ValidateAsync(ctx);
                        }
                    };
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtRegisteredClaimNames.Jti,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = appSettings.Jwt.Issuer,
                        ValidAudience = appSettings.Jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(appSettings.Jwt.Key))
                    };
                });
            return services;
        }
    }
}
