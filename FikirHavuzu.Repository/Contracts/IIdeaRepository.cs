using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;

namespace FikirHavuzu.Repository.Contracts
{
    public interface IIdeaRepository : IRepositoryBase<Idea>
    {
        Task<IEnumerable<Idea>> GetAllIdeasWithDetailsAsync(IdeaRequestParameters p, bool trackChanges);
        Task<int> GetCountAsync(IdeaRequestParameters p);
        Task<Idea> GetIdeaByIdWithDetailsAsync(int ideaId, bool trackChanges);
    }
}