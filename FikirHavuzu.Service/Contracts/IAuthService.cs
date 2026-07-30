using FikirHavuzu.Entity.Dtos.Auth;

namespace FikirHavuzu.Service.Contracts
{
    public interface IAuthService
    {
        Task<UserLoginResponseDto> LoginAsync(UserLoginDto dto);
    }
}
