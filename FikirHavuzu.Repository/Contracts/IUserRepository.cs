using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;

namespace FikirHavuzu.Repository.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetUserWithPermissionsByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersWithDetailsAsync(UserRequestParameters p, bool trackChanges);
        Task<int> GetCountAsync(UserRequestParameters p);
        Task<User> GetUserWithPermissionDetailsAsync(int userId, bool trackChanges);
        Task<User> GetOneUserByIdAsync(int id, bool trackChanges);
    }
}