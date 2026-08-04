using System.ComponentModel.DataAnnotations;

namespace FikirHavuzu.Entity.Dtos.User
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
