using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceManager _manager;

        public HomeController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize(Policy = "IdeaViewPolicy")]
        public IActionResult Index(IdeaRequestParameters p)
        {
            var ideas = _manager.IdeaService.GetAllIdeasWithDetails(p, trackChanges: false);

            var totalCount = _manager.IdeaService.GetCount(p);

            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = totalCount
            };

            var viewModel = new IdeaListViewModel()
            {
                Ideas = ideas,
                Pagination = pagination
            };

            return View(viewModel);
        }
    }
}
