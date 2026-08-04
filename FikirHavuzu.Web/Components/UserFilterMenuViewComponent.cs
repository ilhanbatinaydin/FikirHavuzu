using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FikirHavuzu.Web.Components
{
    public class UserFilterMenuViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public UserFilterMenuViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var permissions = await _manager.PermissionService.GetAllPermissionsForFilterAsync(false);

            var requestQuery = HttpContext.Request.Query;

            var viewModel = new UserFilterViewModel
            {
                FullName = requestQuery["fullName"],
                IdentityNumber = requestQuery["identityNumber"],
                PermissionId = int.TryParse(requestQuery["permissionId"], out int permId) ? permId : null,
                IsActive = bool.TryParse(requestQuery["isActive"], out bool isActive) ? isActive : null
            };

            viewModel.PermissionList = new SelectList(permissions, "Id", "Name", viewModel.PermissionId);

            return View(viewModel);
        }
    }
}
