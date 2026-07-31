using FikirHavuzu.Entity.Dtos.User;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceManager _manager;

        public UserController(IServiceManager manager)
        {
            _manager = manager;
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

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "UserManagePolicy")]
        public IActionResult Create()
        {
            var initialDto = new UserCreateDto { IsActive = true };
            return View(initialDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "UserManagePolicy")]
        public async Task<IActionResult> Create(UserCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _manager.UserService.CreateUserAsync(model);

            TempData["SuccessMessage"] = "Kullanıcı başarıyla eklendi ve varsayılan yetkisi tanımlandı.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Policy = "UserManagePolicy")]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var model = await _manager.UserService.GetUserForUpdateAsync(id, false);

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "UserManagePolicy")]
        public async Task<IActionResult> Update(UserUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _manager.UserService.UpdateUserAsync(model);

                TempData["SuccessMessage"] = $"{model.FirstName} {model.LastName} kullanıcısı başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Güncelleme sırasında bir hata oluştu: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Policy = "PermissionManagePolicy")]
        public async Task<IActionResult> ManagePermissions(int id)
        {
            var model = await _manager.UserService.GetUserForPermissionAssignmentAsync(id);

            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "PermissionManagePolicy")]
        public async Task<IActionResult> ManagePermissions(UserPermissionAssignmentDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _manager.UserService.UpdateUserPermissionsAsync(model.Id, model.SelectedPermissionIds);

            TempData["SuccessMessage"] = "Kullanıcı yetkileri başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
