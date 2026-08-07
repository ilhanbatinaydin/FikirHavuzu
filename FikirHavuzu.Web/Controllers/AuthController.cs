using FikirHavuzu.Entity.Dtos.Auth;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Service.Exceptions;
using FikirHavuzu.Web.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FikirHavuzu.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IServiceManager _manager;
        public AuthController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string returnUrl = "/")
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model, [FromServices] IValidator<LoginViewModel> validator)
        {
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            try
            {
                var loginDto = new UserLoginDto
                {
                    Email = model.Email,
                    Password = model.Password
                };

                var userResponse = await _manager.AuthService.LoginAsync(loginDto);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userResponse.Id.ToString()),
                    new Claim(ClaimTypes.Name, userResponse.FirstName),
                    new Claim(ClaimTypes.Surname, userResponse.LastName),
                    new Claim(ClaimTypes.Email, userResponse.Email)
                };

                foreach (var permission in userResponse.Permissions)
                {
                    claims.Add(new Claim("Permission", permission));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                if (Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (AuthenticationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Sisteme giriş yapılırken beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout([FromForm(Name = "ReturnUrl")] string returnUrl = "/")
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
