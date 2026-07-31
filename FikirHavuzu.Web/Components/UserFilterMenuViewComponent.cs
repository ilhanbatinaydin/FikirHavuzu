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

        public IViewComponentResult Invoke()
        {
            var permissions = _manager.PermissionService.GetAllPermissionsForFilter(false);
            return View(permissions);
        }
    }
}
