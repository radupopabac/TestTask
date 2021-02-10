using Identity.Core.Entities;
using Identity.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Infrastructure
{
    public class TokenValidation : ITokenValidation
    {
        private readonly UserManager<UserEntity> userManager;
        private int userId;
        public TokenValidation(UserManager<UserEntity> userManager)
        {
            this.userManager = userManager;
        }
        public async Task ValidateAsync(TokenValidatedContext ctx)
        {
            if (!int.TryParse(ctx.Principal.Claims.First(x => x.Type == "UserId").Value, out userId))
            {
                throw new UnauthorizedAccessException();
            }

            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            //TODO some other complicated logic to add claims or roles or what is needed
        }

        public int GetUserId() => userId;
    }
}
