using Identity.Core.Entities;

namespace Identity.Core.Interfaces
{
    public interface ITokenGeneration
    {
        string GenerateJwtToken(UserEntity user);
    }
}
