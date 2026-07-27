namespace FikirHavuzu.Repository.Contracts
{
    public interface IRepositoryManager
    {
        ICategoryRepository Category { get; }
        IIdeaRepository Idea { get; }
        IUserRepository User { get; }
        IEvaluationRepository Evaluation { get; }
        void Save();
    }
}