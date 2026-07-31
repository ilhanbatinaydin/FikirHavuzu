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

        public IEnumerable<User> GetAllUsersWithDetails(UserRequestParameters p, bool trackChanges)
        {
            var query = trackChanges ? _context.Users : _context.Users.AsNoTracking();

            return query
                .FilteredByFullName(p.FullName)
                .FilteredByIdentityNumber(p.IdentityNumber)
                .FilteredByActiveStatus(p.IsActive)
                .FilteredByPermissionId(p.PermissionId)
                .Include(u => u.UserPermissions)
                    .ThenInclude(up => up.Permission)
                .OrderByDescending(u => u.Id)
                .ToPaginate(p.PageNumber, p.PageSize)
                .ToList();
        }

        public int GetCount(UserRequestParameters p)
        {
            return _context.Users.AsNoTracking()
                .FilteredByFullName(p.FullName)
                .FilteredByIdentityNumber(p.IdentityNumber)
                .FilteredByActiveStatus(p.IsActive)
                .FilteredByPermissionId(p.PermissionId)
                .Count();
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