using AutoMapper;
using FikirHavuzu.Entity.Dtos.User;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Service.Exceptions;
using FikirHavuzu.Web.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FikirHavuzu.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public UserController(IServiceManager manager, IMapper mapper, IAuthorizationService authorizationService)
        {
            _manager = manager;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize(Policy = "UserManagePolicy")]
        public async Task<IActionResult> Index(UserRequestParameters p)
        {
            var users = await _manager.UserService.GetAllUsersWithDetailsAsync(p, trackChanges: false);
            var totalCount = await _manager.UserService.GetCountAsync(p);

            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = totalCount
            };

            var viewModel = new UserListViewModel()
            {
                Users = users,
                Pagination = pagination
            };

            if (Request.Headers.ContainsKey("HX-Request"))
            {
                return PartialView("_UserListPartial", viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "UserManagePolicy")]
        public IActionResult Create()
        {
            var initialViewModel = new UserCreateViewModel { IsActive = true };
            return View(initialViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "UserManagePolicy")]
        public async Task<IActionResult> Create(UserCreateViewModel model, [FromServices] IValidator<UserCreateViewModel> validator)
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
                var userCreateDto = _mapper.Map<UserCreateDto>(model);
                await _manager.UserService.CreateUserAsync(userCreateDto);

                TempData["SuccessMessage"] = "Kullanıcı başarıyla eklendi ve varsayılan yetkisi tanımlandı.";
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı kaydedilirken sunucu kaynaklı beklenmeyen bir hata oluştu. Lütfen sistem yöneticisiyle iletişime geçin.");

                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Policy = "UserManagePolicy")]
        [Route("user/{id}/update")]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var dto = await _manager.UserService.GetUserForUpdateAsync(id, false);
                var viewModel = _mapper.Map<UserUpdateViewModel>(dto);

                return View(viewModel);
            }
            catch (NotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Kullanıcı bilgileri getirilirken beklenmeyen bir hata oluştu.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "UserManagePolicy")]
        public async Task<IActionResult> Update(UserUpdateViewModel model, [FromServices] IValidator<UserUpdateViewModel> validator)
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
                var dto = _mapper.Map<UserUpdateDto>(model);
                await _manager.UserService.UpdateUserAsync(dto);

                TempData["SuccessMessage"] = "Kullanıcı başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Güncelleme işlemi sırasında sunucu kaynaklı beklenmeyen bir hata oluştu.");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Policy = "PermissionManagePolicy")]
        [Route("user/{id}/managepermissions")]
        public async Task<IActionResult> ManagePermissions(int id)
        {
            try
            {
                var dto = await _manager.UserService.GetUserForPermissionAssignmentAsync(id, false);
                var viewModel = _mapper.Map<UserPermissionAssignmentViewModel>(dto);
                return View(viewModel);
            }
            catch (NotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Kullanıcı yetkileri getirilirken sistem kaynaklı bir hata oluştu.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "PermissionManagePolicy")]
        public async Task<IActionResult> ManagePermissions(UserPermissionAssignmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _manager.UserService.UpdateUserPermissionsAsync(model.Id, model.SelectedPermissionIds);
                TempData["SuccessMessage"] = "Kullanıcı yetkileri başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (BusinessRuleException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Yetkilendirme işlemi sırasında sunucu kaynaklı beklenmeyen bir hata oluştu.");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("user/{id}/profile")]
        public async Task<IActionResult> Profile(int id)
        {
            try
            {
                var userDto = await _manager.UserService.GetOneUserByIdAsync(id, false);

                var authResult = await _authorizationService.AuthorizeAsync(User, userDto, "ProfileAccessPolicy");

                if (!authResult.Succeeded)
                {
                    TempData["ErrorMessage"] = "Başka bir kullanıcının profilini görüntüleme yetkiniz bulunmamaktadır.";
                    return RedirectToAction("Index", "Home");
                }

                return View(userDto);
            }
            catch (NotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Kullanıcı bilgileri yüklenirken sistemsel bir hata oluştu.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
