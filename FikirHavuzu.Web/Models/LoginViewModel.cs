using System.ComponentModel.DataAnnotations;

namespace FikirHavuzu.Web.Models
{
    public class LoginViewModel
    {
        private string? _returnUrl;

        [Display(Name = "E-posta Adresi")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Şifre")]
        public string Password { get; set; } = string.Empty;

        public string ReturnUrl
        {
            get
            {
                if (_returnUrl is null)
                {
                    return "/";
                }
                else
                {
                    return _returnUrl;
                }
            }
            set
            {
                _returnUrl = value;
            }
        }
    }
}