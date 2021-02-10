using Identity.Core.AppSettings;
using Identity.Core.Entities;
using Identity.Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Infrastructure
{
    public class TokenGeneration : ITokenGeneration
    {
        private readonly AppSettings appSettings;

        public TokenGeneration(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }
        public string GenerateJwtToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(30);

            var token = new JwtSecurityToken(appSettings.Jwt.Issuer,
                appSettings.Jwt.Audience,
                claims,
                expires: expires,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
