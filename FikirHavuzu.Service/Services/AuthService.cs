using AutoMapper;
using FikirHavuzu.Entity.Dtos.Auth;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Service.Contracts;

namespace FikirHavuzu.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager _manager;

        private readonly IMapper _mapper;

        public AuthService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<UserLoginResponseDto> LoginAsync(UserLoginDto dto)
        {
            var user=await _manager.User.GetUserWithPermissionsByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new Exception("E-posta adresi veya şifre hatalı.");
            }

            if (!user.IsActive)
            {
                throw new Exception("Hesabınız pasif duruma alınmıştır.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("E-posta adresi veya şifre hatalı.");
            }

            var responseDto = _mapper.Map<UserLoginResponseDto>(user);

            return responseDto;
        }
    }
}
