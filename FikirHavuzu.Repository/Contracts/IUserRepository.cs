using FikirHavuzu.Entity.Entities;

namespace FikirHavuzu.Repository.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetUserWithPermissionsByEmailAsync(string email);
    }
}