using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;

namespace FikirHavuzu.Repository.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext context) : base(context)
        {

        }
    }
}
