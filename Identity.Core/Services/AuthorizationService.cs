using Identity.Core.Entities;
using Identity.Core.Interfaces;
using Identity.Core.Interfaces.Services;
using Identity.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Identity.Core.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<UserEntity> userManager;
        private readonly ILogger<AuthorizationService> logger;
        private readonly SignInManager<UserEntity> signInManager;
        private readonly ITokenGeneration tokenGeneration;

        public AuthorizationService(UserManager<UserEntity> userManager, ILogger<AuthorizationService> logger, SignInManager<UserEntity> signInManager, ITokenGeneration tokenGeneration)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.signInManager = signInManager;
            this.tokenGeneration = tokenGeneration;
        }
        public async Task<string> AuthorizeAsync(EmailAndPasswordModel emailAndPassword)
        {
            var user = await userManager.FindByEmailAsync(emailAndPassword.Email);
            if (user == null) throw new UnauthorizedAccessException();

            var result = await signInManager.PasswordSignInAsync(user.UserName, emailAndPassword.Password, false, false);

            if (result.Succeeded) return tokenGeneration.GenerateJwtToken(user);

            logger.LogWarning($"Authentication failed for username {user.UserName}");
            throw new UnauthorizedAccessException();
        }
    }
}
