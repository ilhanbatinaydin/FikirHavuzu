using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            var requestQuery = HttpContext.Request.Query;

            var viewModel = new IdeaFilterViewModel
            {
                SearchQuery = requestQuery["searchQuery"],
                FullName = requestQuery["fullName"],
                CategoryId = int.TryParse(requestQuery["categoryId"], out int catId) ? catId : null,
                StartDate = DateTime.TryParse(requestQuery["StartDate"], out DateTime sDate) ? sDate : null,
                EndDate = DateTime.TryParse(requestQuery["EndDate"], out DateTime eDate) ? eDate : null
            };

            viewModel.CategoryList = new SelectList(categories, "Id", "Name", viewModel.CategoryId);

            return View(viewModel);
        }
    }
}
