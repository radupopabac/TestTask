using Identity.Core.Entities;
using Identity.Core.Repositories;

namespace Identity.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public AddressEntity Save(AddressEntity address)
        {
            _context.SaveChanges();
            return address;
        }
    }
}