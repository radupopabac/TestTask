using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace Identity.Core.Interfaces
{
    public interface ITokenValidation
    {
        Task ValidateAsync(TokenValidatedContext ctx);
        int GetUserId();
    }
}
