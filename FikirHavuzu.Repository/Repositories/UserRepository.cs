using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FikirHavuzu.Repository.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersWithDetailsAsync(UserRequestParameters p, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .FilteredByFullName(p.FullName)
                .FilteredByEmail(p.Email)
                .FilteredByIdentityNumber(p.IdentityNumber)
                .FilteredByActiveStatus(p.IsActive)
                .FilteredByPermissionId(p.PermissionId)
                .Include(u => u.UserPermissions)
                    .ThenInclude(up => up.Permission)
                .OrderBy(u => u.Id)
                .ToPaginate(p.PageNumber, p.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(UserRequestParameters p)
        {
            return await FindAll(false)
                .FilteredByFullName(p.FullName)
                .FilteredByEmail(p.Email)
                .FilteredByIdentityNumber(p.IdentityNumber)
                .FilteredByActiveStatus(p.IsActive)
                .FilteredByPermissionId(p.PermissionId)
                .CountAsync();  
        }

        public async Task<User?> GetUserWithPermissionsByEmailAsync(string email, bool trackChanges)
        {
            return await FindByCondition(u => u.Email == email && u.IsActive, trackChanges)
                .Include(u => u.UserPermissions)
                    .ThenInclude(up => up.Permission)
                .FirstOrDefaultAsync();
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
                .Include(u => u.UserPermissions)
                    .ThenInclude(up => up.Permission)
                .SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByConditionAsync(Expression<Func<User, bool>> expression, bool trackChanges)
        {
            return await FindByCondition(expression, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<bool> CheckIfUserExistsAsync(Expression<Func<User, bool>> expression)
        {
            return await FindByCondition(expression, trackChanges: false).AnyAsync();
        }

    }
}