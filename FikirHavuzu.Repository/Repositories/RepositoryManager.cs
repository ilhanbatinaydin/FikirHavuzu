using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;

namespace FikirHavuzu.Repository.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIdeaRepository _ideaRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEvaluationRepository _evaluationRepository;
        private readonly IPermissionRepository _permissionRepository;

        public RepositoryManager(AppDbContext context, ICategoryRepository categoryRepository, IIdeaRepository ideaRepository, IUserRepository userRepository, IEvaluationRepository evaluationRepository, IPermissionRepository permissionRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _ideaRepository = ideaRepository;
            _userRepository = userRepository;
            _evaluationRepository = evaluationRepository;
            _permissionRepository = permissionRepository;
        }

        public ICategoryRepository Category => _categoryRepository;
        public IIdeaRepository Idea => _ideaRepository;
        public IUserRepository User => _userRepository;
        public IEvaluationRepository Evaluation => _evaluationRepository;
        public IPermissionRepository Permission => _permissionRepository;

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}