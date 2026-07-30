using FikirHavuzu.Service.Contracts;

namespace FikirHavuzu.Service.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IAuthService _authService;

        private readonly ICategoryService _categoryService;

        private readonly IIdeaService _ideaService;

        public ServiceManager(IAuthService authService, ICategoryService categoryService, IIdeaService ideaService)
        {
            _authService = authService;
            _categoryService = categoryService;
            _ideaService = ideaService;
        }

        public IAuthService AuthService => _authService;

        public ICategoryService CategoryService => _categoryService;

        public IIdeaService IdeaService => _ideaService;
    }
}
