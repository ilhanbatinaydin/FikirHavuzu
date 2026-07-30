using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;

namespace FikirHavuzu.Repository.Contracts
{
    public interface IIdeaRepository : IRepositoryBase<Idea>
    {
        IQueryable<Idea> GetAllIdeas(bool trackChanges);
        IQueryable<Idea> GetAllIdeasWithDetails(IdeaRequestParameters p, bool trackChanges);
        Idea? GetOneIdea(int id, bool trackChanges);
        int GetCount(IdeaRequestParameters p);
    }
}