using FikirHavuzu.Entity.Dtos.User;
using FikirHavuzu.Entity.RequestParameters;

namespace FikirHavuzu.Service.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersWithDetailsAsync(UserRequestParameters p, bool trackChanges);
        Task<int> GetCountAsync(UserRequestParameters p);
        Task CreateUserAsync(UserCreateDto userDto);
        Task UpdateUserAsync(UserUpdateDto userDto);
        Task<UserUpdateDto> GetUserForUpdateAsync(int id, bool trackChanges);
        Task<IEnumerable<int>> GetUserPermissionIdsAsync(int userId);
        Task UpdateUserPermissionsAsync(int userId, List<int> selectedPermissionIds);
        Task<UserDto> GetOneUserByIdAsync(int id, bool trackChanges);
        Task<UserPermissionAssignmentDto> GetUserForPermissionAssignmentAsync(int id);
        Task<bool> IsIdentityExistsAsync(string identityNumber, int? excludeUserId = null);
        Task<bool> IsRegistrationNumberExistsAsync(string registrationNumber, int? excludeUserId = null);
        Task<bool> IsEmailExistsAsync(string email, int? excludeUserId = null);
        Task<bool> IsPhoneNumberExistsAsync(string phoneNumber, int? excludeUserId = null);
    }
}
