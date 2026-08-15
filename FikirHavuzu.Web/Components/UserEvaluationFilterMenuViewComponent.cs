using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FikirHavuzu.Web.Components
{
    public class UserEvaluationFilterMenuViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public UserEvaluationFilterMenuViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var categories = await _manager.CategoryService.GetAllCategoriesAsync(false);
            var requestQuery = HttpContext.Request.Query;

            var model = new UserEvaluationFilterViewModel
            {
                UserId = userId,
                SearchQuery = requestQuery["SearchQuery"],
                CategoryId = int.TryParse(requestQuery["CategoryId"], out int catId) ? catId : null,
                Comment = requestQuery["Comment"],
                Score = int.TryParse(requestQuery["Score"], out int s) ? s : null,
                IsApproved = bool.TryParse(requestQuery["IsApproved"], out bool isApp) ? isApp : null,
                StartDate = DateTime.TryParse(requestQuery["StartDate"], out DateTime sd) ? sd : null,
                EndDate = DateTime.TryParse(requestQuery["EndDate"], out DateTime ed) ? ed : null
            };

            model.CategoryList = new SelectList(categories, "Id", "Name", model.CategoryId);

            return View(model);
        }
    }
}
