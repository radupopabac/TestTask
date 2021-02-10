using Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Core.Interfaces.Services
{
    public interface IUserManagerService
    {
        Task<IEnumerable<IdentityError>> DeleteAsync();
        Task<IEnumerable<IdentityError>> CreateAsync(UserEntity request, string password);
    }
}