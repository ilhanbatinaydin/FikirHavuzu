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

        public RepositoryManager(AppDbContext context, ICategoryRepository categoryRepository, IIdeaRepository ideaRepository, IUserRepository userRepository, IEvaluationRepository evaluationRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _ideaRepository = ideaRepository;
            _userRepository = userRepository;
            _evaluationRepository = evaluationRepository;
        }

        public ICategoryRepository Category => _categoryRepository;
        public IIdeaRepository Idea => _ideaRepository;
        public IUserRepository User => _userRepository;
        public IEvaluationRepository Evaluation => _evaluationRepository;

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