using FikirHavuzu.Entity.Dtos.User;

namespace FikirHavuzu.Service.Contracts
{
    public interface IPermissionService
    {
        IEnumerable<PermissionDto> GetAllPermissionsForFilter(bool trackChanges);
    }
}
