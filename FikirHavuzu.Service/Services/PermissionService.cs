using AutoMapper;
using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.Dtos.User;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Service.Contracts;

namespace FikirHavuzu.Service.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepositoryManager _manager;

        private readonly IMapper _mapper;

        public PermissionService(IRepositoryManager manager, IMapper mapper)
        {
            _manager= manager;
            _mapper= mapper;
        }

        public IEnumerable<PermissionDto> GetAllPermissionsForFilter(bool trackChanges)
        {
            var permissions = _manager.Permission.FindAll(trackChanges);

            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }
    }
}
