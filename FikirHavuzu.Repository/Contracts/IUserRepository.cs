using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;

namespace FikirHavuzu.Repository.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetUserWithPermissionsByEmailAsync(string email);
        IEnumerable<User> GetAllUsersWithDetails(UserRequestParameters p, bool trackChanges);
        int GetCount(UserRequestParameters p);

    }
}