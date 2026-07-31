using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceManager _manager;

        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Authorize(Policy = "UserManagePolicy")]
        public IActionResult Index(UserRequestParameters p)
        {
            var users = _manager.UserService.GetAllUsersWithDetails(p, trackChanges: false);

            var totalCount = _manager.UserService.GetCount(p);

            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = totalCount
            };

            var viewModel = new UserListViewModel()
            {
                Users = users,
                Pagination = pagination
            };

            return View(viewModel);
        }
    }
}
