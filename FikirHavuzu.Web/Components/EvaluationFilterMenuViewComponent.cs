using FikirHavuzu.Entity.RequestParameters;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Components
{
    public class EvaluationFilterMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int ideaId)
        {
            var parameters = new EvaluationRequestParameters
            {
                IdeaId = ideaId
            };

            return View(parameters);
        }
    }
}
