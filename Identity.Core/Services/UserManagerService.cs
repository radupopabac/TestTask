using Identity.Core.Entities;
using Identity.Core.Interfaces;
using Identity.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Identity.Core.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly UserManager<UserEntity> userManager;
        private readonly ILogger<UserManagerService> logger;
        private readonly ITokenValidation tokenValidation;

        public UserManagerService(UserManager<UserEntity> userManager, ILogger<UserManagerService> logger, ITokenValidation tokenValidation)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.tokenValidation = tokenValidation;
        }
        public async Task<IEnumerable<IdentityError>> CreateAsync(UserEntity user, string pasword)
        {
            var existingUser = await userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new UnauthorizedAccessException();
            }

            //TODO instead of this we should send an email for the user to confirm his identity
            user.EmailConfirmed = true;
            var savedUser = await userManager.CreateAsync(user, pasword);

            if (!savedUser.Succeeded)
            {
                logger.LogCritical($"User creation failed {JsonSerializer.Serialize(savedUser.Errors)}");
                return savedUser.Errors;
            }

            //TODO create user profile on profile microservice.
            return null;
        }

        public async Task<IEnumerable<IdentityError>> DeleteAsync()
        {
            var userId = tokenValidation.GetUserId();
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            var deleteResult = await userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                logger.LogCritical($"User deletion failed {JsonSerializer.Serialize(deleteResult.Errors)}");
                return deleteResult.Errors;
            }
            return null;
        }
    }
}