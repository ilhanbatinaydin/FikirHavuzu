using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Repository.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersWithDetailsAsync(UserRequestParameters p, bool trackChanges)
        {
            var query = trackChanges ? _context.Users : _context.Users.AsNoTracking();

            return await query
                .FilteredByFullName(p.FullName)
                .FilteredByIdentityNumber(p.IdentityNumber)
                .FilteredByActiveStatus(p.IsActive)
                .FilteredByPermissionId(p.PermissionId)
                .Include(u => u.UserPermissions)
                    .ThenInclude(up => up.Permission)
                .OrderByDescending(u => u.Id)
                .ToPaginate(p.PageNumber, p.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(UserRequestParameters p)
        {
            return await _context.Users.AsNoTracking()
                .FilteredByFullName(p.FullName)
                .FilteredByIdentityNumber(p.IdentityNumber)
                .FilteredByActiveStatus(p.IsActive)
                .FilteredByPermissionId(p.PermissionId)
                .CountAsync();
        }

        public async Task<User?> GetUserWithPermissionsByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.UserPermissions)
                    .ThenInclude(up => up.Permission)
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
        }

        public async Task<User> GetUserWithPermissionDetailsAsync(int userId, bool trackChanges)
        {
            return await FindByCondition(u => u.Id == userId, trackChanges)
                        .Include(u => u.UserPermissions)
                        .SingleOrDefaultAsync();
        }
        public async Task<User> GetOneUserByIdAsync(int id, bool trackChanges)
        {
            return await FindByCondition(u => u.Id == id, trackChanges)
                        .SingleOrDefaultAsync();
        }

    }
}