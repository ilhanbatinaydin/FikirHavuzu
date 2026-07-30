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

        public IViewComponentResult Invoke()
        {
            var categories=_manager.CategoryService.GetAllCategories(false);
            return View(categories);
        }
    }
}
