using FikirHavuzu.Entity.Dtos.User;
using FikirHavuzu.Entity.RequestParameters;

namespace FikirHavuzu.Service.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsersWithDetails(UserRequestParameters p, bool trackChanges);
        int GetCount(UserRequestParameters p);

    }
}
