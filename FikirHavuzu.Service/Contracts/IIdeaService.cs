using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.RequestParameters;

namespace FikirHavuzu.Service.Contracts
{
    public interface IIdeaService
    {
        Task<IEnumerable<IdeaDto>> GetAllIdeasWithDetailsAsync(IdeaRequestParameters p, bool trackChanges);

        Task<int> GetCountAsync(IdeaRequestParameters p);

        Task CreateIdeaAsync(IdeaCreateDto ideaDto, int userId);

        Task<IdeaDetailDto> GetIdeaByIdWithDetailsAsync(int ideaId, bool trackChanges);
    }
}
