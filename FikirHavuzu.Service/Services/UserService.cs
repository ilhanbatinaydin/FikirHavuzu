using AutoMapper;
using FikirHavuzu.Entity.Dtos.User;
using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

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

        public async Task CreateUserAsync(UserCreateDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user.PasswordHash=BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var defaultPermission = await _manager.Permission
                .FindByCondition(p => p.Name == "idea.view", trackChanges: false)
                .FirstOrDefaultAsync();

            if (defaultPermission == null)
            {
                throw new NotFoundException("Sistemde varsayılan kullanıcı yetkisi (idea.view) bulunamadı. Lütfen sistem yöneticisine başvurun.");
            }

            user.UserPermissions = new List<UserPermission>
            {
                new UserPermission { PermissionId = defaultPermission.Id }
            };

            _manager.User.Create(user);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersWithDetailsAsync(UserRequestParameters p, bool trackChanges)
        {
            var users = await _manager.User.GetAllUsersWithDetailsAsync(p, trackChanges);

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<int> GetCountAsync(UserRequestParameters p)
        {
            return await _manager.User.GetCountAsync(p);
        }

        public async Task<UserUpdateDto> GetUserForUpdateAsync(int id, bool trackChanges)
        {
            var user = await _manager.User
                .FindByCondition(u => u.Id == id, trackChanges)
                .SingleOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException("Güncellenmek istenen kullanıcı bulunamadı.");
            }

            return _mapper.Map<UserUpdateDto>(user);
        }

        public async Task UpdateUserAsync(UserUpdateDto userDto)
        {
            var user = await _manager.User
                .FindByCondition(u => u.Id == userDto.Id, trackChanges: true)
                .SingleOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException("Güncellenmek istenen kullanıcı bulunamadı.");
            }

            var existingPasswordHash = user.PasswordHash;

            _mapper.Map(userDto, user);

            if (string.IsNullOrEmpty(userDto.Password))
            {
                user.PasswordHash = existingPasswordHash;
            }
            else
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }

            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<int>> GetUserPermissionIdsAsync(int userId)
        {

            var user = await _manager.User.GetUserWithPermissionDetailsAsync(userId, trackChanges: false);

            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            return user.UserPermissions.Select(up => up.PermissionId).ToList();
        }

        public async Task UpdateUserPermissionsAsync(int userId, List<int> selectedPermissionIds)
        {
            var user = await _manager.User.GetUserWithPermissionDetailsAsync(userId, trackChanges: true);

            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            if (selectedPermissionIds != null && selectedPermissionIds.Any())
            {
                var allPermissions = await _manager.Permission.GetAllPermissionsWithDependenciesAsync(trackChanges: false);

                foreach (var selectedId in selectedPermissionIds)
                {
                    var permission = allPermissions.FirstOrDefault(p => p.Id == selectedId);

                    if (permission != null && permission.RequiredPermissions.Any())
                    {
                        foreach (var required in permission.RequiredPermissions)
                        {
                            if (!selectedPermissionIds.Contains(required.RequiredPermissionId))
                            {
                                throw new BusinessRuleException($"Güvenlik İhlali: '{permission.Name}' yetkisi, ID'si {required.RequiredPermissionId} olan temel yetki olmadan atanamaz!");
                            }
                        }
                    }
                }
            }

            user.UserPermissions.Clear();

            if (selectedPermissionIds != null && selectedPermissionIds.Any())
            {
                foreach (var permissionId in selectedPermissionIds)
                {
                    user.UserPermissions.Add(new UserPermission
                    {
                        UserId = userId,
                        PermissionId = permissionId
                    });
                }
            }

            await _manager.SaveAsync();
        }

        public async Task<UserDto> GetOneUserByIdAsync(int id, bool trackChanges)
        {
            var user = await _manager.User.GetOneUserByIdAsync(id, trackChanges);

            if (user == null)
                throw new NotFoundException($"ID'si {id} olan kullanıcı bulunamadı.");

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<UserPermissionAssignmentDto> GetUserForPermissionAssignmentAsync(int id)
        {
            var userEntity = await _manager.User.GetUserWithPermissionDetailsAsync(id, trackChanges: false);

            if (userEntity == null)
            {
                throw new NotFoundException("Yetkileri düzenlenecek kullanıcı bulunamadı.");
            }

            var model = _mapper.Map<UserPermissionAssignmentDto>(userEntity);
            model.SelectedPermissionIds = userEntity.UserPermissions.Select(up => up.PermissionId).ToList();

            return model;
        }

        public async Task<bool> IsIdentityExistsAsync(string identityNumber, int? excludeUserId = null)
        {
            return await _manager.User.FindByCondition(u =>
                u.IdentityNumber == identityNumber &&
                (!excludeUserId.HasValue || u.Id != excludeUserId.Value), trackChanges: false)
                .AnyAsync();
        }

        public async Task<bool> IsRegistrationNumberExistsAsync(string registrationNumber, int? excludeUserId = null)
        {
            return await _manager.User.FindByCondition(u =>
                u.RegistrationNumber == registrationNumber &&
                (!excludeUserId.HasValue || u.Id != excludeUserId.Value), trackChanges: false)
                .AnyAsync();
        }

        public async Task<bool> IsEmailExistsAsync(string email, int? excludeUserId = null)
        {
            return await _manager.User.FindByCondition(u =>
                u.Email == email &&
                (!excludeUserId.HasValue || u.Id != excludeUserId.Value), trackChanges: false)
                .AnyAsync();
        }

        public async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber, int? excludeUserId = null)
        {
            return await _manager.User.FindByCondition(u =>
                u.PhoneNumber == phoneNumber &&
                (!excludeUserId.HasValue || u.Id != excludeUserId.Value), trackChanges: false)
                .AnyAsync();
        }

    }
}
