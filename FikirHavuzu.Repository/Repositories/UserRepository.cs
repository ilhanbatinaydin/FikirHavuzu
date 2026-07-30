using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Repository.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserWithPermissionsByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.UserPermissions)
                    .ThenInclude(up => up.Permission)
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
        }
    }
}