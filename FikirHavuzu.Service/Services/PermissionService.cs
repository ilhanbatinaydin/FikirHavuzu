using AutoMapper;
using FikirHavuzu.Entity.Dtos.User;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Service.Contracts;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<PermissionDto>> GetAllPermissionsForFilterAsync(bool trackChanges)
        {
            var permissions = await _manager.Permission.FindAll(trackChanges).ToListAsync();

            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }

        public async Task<IEnumerable<PermissionWithDependenciesDto>> GetAllPermissionsWithDependenciesAsync(bool trackChanges)
        {
            var permissions = await _manager.Permission.GetAllPermissionsWithDependenciesAsync(trackChanges);

            var permissionsDto = _mapper.Map<IEnumerable<PermissionWithDependenciesDto>>(permissions);

            return permissionsDto;
        }
    }
}
