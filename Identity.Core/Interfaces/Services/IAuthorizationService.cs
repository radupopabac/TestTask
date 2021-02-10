using Identity.Core.ViewModels;
using System.Threading.Tasks;

namespace Identity.Core.Interfaces.Services
{
    public interface IAuthorizationService
    {
        Task<string> AuthorizeAsync(EmailAndPasswordModel emailAndPassword);
    }
}
