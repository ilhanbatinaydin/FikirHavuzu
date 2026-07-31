using FikirHavuzu.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

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
            // 2. Servis metodunu asenkron (Async) versiyonuyla değiştirip await ile bekliyoruz
            var permissions = await _manager.PermissionService.GetAllPermissionsForFilterAsync(false);

            return View(permissions);
        }
    }
}
