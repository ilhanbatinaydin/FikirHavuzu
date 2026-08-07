using FikirHavuzu.Entity.Dtos.User;

namespace FikirHavuzu.Service.Contracts
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAllPermissionsForFilterAsync(bool trackChanges);

        Task<IEnumerable<PermissionWithDependenciesDto>> GetAllPermissionsWithDependenciesAsync(bool trackChanges);
    }
}
