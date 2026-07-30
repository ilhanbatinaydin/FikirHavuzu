namespace FikirHavuzu.Entity.Dtos.Auth
{
    public class UserLoginResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
