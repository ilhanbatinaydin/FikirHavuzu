using FikirHavuzu.Entity.Entities;

namespace FikirHavuzu.Repository.Contracts
{
    public interface IPermissionRepository : IRepositoryBase<Permission>
    {
        Task<IEnumerable<Permission>> GetAllPermissionsWithDependenciesAsync(bool trackChanges);
    }
}
