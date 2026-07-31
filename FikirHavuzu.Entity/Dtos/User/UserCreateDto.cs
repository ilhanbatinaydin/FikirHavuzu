using System.ComponentModel.DataAnnotations;

namespace FikirHavuzu.Entity.Dtos.User
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "T.C. Kimlik Numarası zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "T.C. Kimlik Numarası 11 haneli olmalıdır.")]
        public string IdentityNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sicil Numarası alanı zorunludur.")]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [Compare("Password", ErrorMessage = "Şifreler birbiriyle uyuşmuyor.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
