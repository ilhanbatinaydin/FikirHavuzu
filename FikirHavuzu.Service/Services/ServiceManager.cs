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

        private readonly IEvaluationService _evaluationService;

        public ServiceManager(IAuthService authService, ICategoryService categoryService, IIdeaService ideaService, IUserService userService, IPermissionService permissionService, IEvaluationService evaluationService)
        {
            _authService = authService;
            _categoryService = categoryService;
            _ideaService = ideaService;
            _userService = userService;
            _permissionService = permissionService;
            _evaluationService = evaluationService;
        }

        public IAuthService AuthService => _authService;

        public ICategoryService CategoryService => _categoryService;

        public IIdeaService IdeaService => _ideaService;

        public IUserService UserService => _userService;

        public IPermissionService PermissionService => _permissionService;

        public IEvaluationService EvaluationService => _evaluationService;
    }
}
