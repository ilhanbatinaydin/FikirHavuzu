using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;

namespace FikirHavuzu.Repository.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IIdeaRepository> _ideaRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IEvaluationRepository> _evaluationRepository;

        public RepositoryManager(AppDbContext context)
        {
            _context = context;

            // Lazy Loading: Repository'ler sadece ihtiyaç duyulduğu an (çağrıldığı an) new'lenir.
            // Bu sayede gereksiz bellek kullanımı engellenir.
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
            _ideaRepository = new Lazy<IIdeaRepository>(() => new IdeaRepository(_context));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _evaluationRepository = new Lazy<IEvaluationRepository>(() => new EvaluationRepository(_context));
        }

        public ICategoryRepository Category => _categoryRepository.Value;
        public IIdeaRepository Idea => _ideaRepository.Value;
        public IUserRepository User => _userRepository.Value;
        public IEvaluationRepository Evaluation => _evaluationRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}