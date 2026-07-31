using AutoMapper;
using FikirHavuzu.Entity.Dtos.User;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Repository.Repositories;
using FikirHavuzu.Service.Contracts;

namespace FikirHavuzu.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _manager;

        private readonly IMapper _mapper;

        public UserService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public IEnumerable<UserDto> GetAllUsersWithDetails(UserRequestParameters p, bool trackChanges)
        {
            var users = _manager.User.GetAllUsersWithDetails(p, trackChanges);

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public int GetCount(UserRequestParameters p)
        {
            return _manager.User.GetCount(p);
        }
    }
}
