using FikirHavuzu.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Components
{
    public class IdeaFilterMenuViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public IdeaFilterMenuViewComponent(IServiceManager manager)
        {
            _manager= manager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _manager.CategoryService.GetAllCategoriesAsync(false);

            return View(categories);
        }
    }
}
