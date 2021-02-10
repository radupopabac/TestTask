using Identity.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> Get();

        UserEntity Save(UserEntity user);
    }
}