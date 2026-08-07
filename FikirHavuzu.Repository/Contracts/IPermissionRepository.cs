using FikirHavuzu.Entity.Entities;

namespace FikirHavuzu.Repository.Contracts
{
    public interface IPermissionRepository : IRepositoryBase<Permission>
    {
        Task<IEnumerable<Permission>> GetAllPermissionsWithDependenciesAsync(bool trackChanges);

        Task<IEnumerable<Permission>> GetAllPermissionsAsync(bool trackChanges);

        Task<Permission> GetPermissionByNameAsync(string name, bool trackChanges);
    }
}
