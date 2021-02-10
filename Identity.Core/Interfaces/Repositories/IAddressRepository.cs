using Identity.Core.Entities;

namespace Identity.Core.Repositories
{
    public interface IAddressRepository
    {
        AddressEntity Save(AddressEntity address);
    }
}