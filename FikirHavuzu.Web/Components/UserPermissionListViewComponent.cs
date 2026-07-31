using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FikirHavuzu.WebUI.ViewComponents
{
    public class UserPermissionListViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public UserPermissionListViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<int> selectedIds)
        {
            selectedIds ??= new List<int>();

            var permissionsDto = await _manager.PermissionService.GetAllPermissionsWithDependenciesAsync(trackChanges: false);

            var model = new List<PermissionItemViewModel>();

            foreach (var dto in permissionsDto)
            {
                model.Add(new PermissionItemViewModel
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    IsAssigned = selectedIds.Contains(dto.Id),
                    DependencyIdsAsJson = JsonSerializer.Serialize(dto.RequiredPermissionIds)
                });
            }

            return View(model);
        }
    }
}