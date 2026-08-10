using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Components
{
    public class EvaluationListViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public EvaluationListViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(EvaluationRequestParameters parameters)
        {
            var evaluations = await _manager.EvaluationService.GetAllEvaluationsWithDetailsAsync(parameters, trackChanges: false);

            var totalCount = await _manager.EvaluationService.GetCountAsync(parameters);

            var pagination = new Pagination
            {
                CurrentPage = parameters.PageNumber,
                ItemsPerPage = parameters.PageSize,
                TotalItems = totalCount
            };

            var model = new EvaluationListViewModel
            {
                IdeaId = parameters.IdeaId,
                Evaluations = evaluations,
                Pagination = pagination
            };

            return View(model);
        }

    }
}
