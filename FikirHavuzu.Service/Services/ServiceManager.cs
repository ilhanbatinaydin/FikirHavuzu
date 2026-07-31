using FikirHavuzu.Service.Contracts;

namespace FikirHavuzu.Service.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IAuthService _authService;

        private readonly ICategoryService _categoryService;

        private readonly IIdeaService _ideaService;

        private readonly IUserService _userService;

        private readonly IPermissionService _permissionService;

        public ServiceManager(IAuthService authService, ICategoryService categoryService, IIdeaService ideaService, IUserService userService, IPermissionService permissionService)
        {
            _authService = authService;
            _categoryService = categoryService;
            _ideaService = ideaService;
            _userService = userService;
            _permissionService = permissionService;
        }

        public IAuthService AuthService => _authService;

        public ICategoryService CategoryService => _categoryService;

        public IIdeaService IdeaService => _ideaService;

        public IUserService UserService => _userService;

        public IPermissionService PermissionService => _permissionService;
    }
}
