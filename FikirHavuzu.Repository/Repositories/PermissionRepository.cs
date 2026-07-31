using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Repository.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsWithDependenciesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(p => p.RequiredPermissions)
                .ThenInclude(dp => dp.RequiredPermission)
                .ToListAsync();
        }
    }
}
