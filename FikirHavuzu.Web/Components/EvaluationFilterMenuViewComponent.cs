using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Components
{
    public class EvaluationFilterMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int ideaId)
        {
            var requestQuery = HttpContext.Request.Query;

            var model = new EvaluationFilterViewModel
            {
                IdeaId = ideaId,
                FullName = requestQuery["FullName"],
                Email = requestQuery["Email"],
                Comment = requestQuery["Comment"],
                Score = int.TryParse(requestQuery["Score"], out int s) ? s : null,
                IsApproved = bool.TryParse(requestQuery["IsApproved"], out bool isApp) ? isApp : null,
                StartDate = DateTime.TryParse(requestQuery["StartDate"], out DateTime sd) ? sd : null,
                EndDate = DateTime.TryParse(requestQuery["EndDate"], out DateTime ed) ? ed : null
            };

            return View(model);
        }
    }
}
