using Identity.Core.Entities;
using Identity.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<UserEntity>> Get()
        {
             return await _context.Users/*.Include(u => u.AddressEntity)*/.ToArrayAsync();
        }

        public UserEntity Save(UserEntity user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}