using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FikirHavuzu.Service.Contracts
{
    public interface IIdeaService
    {
        IEnumerable<IdeaDto> GetAllIdeasWithDetails(IdeaRequestParameters p, bool trackChanges);

        IEnumerable<IdeaDto> GetAllIdeas(bool trackChanges);

        int GetCount(IdeaRequestParameters p);

        Task CreateIdeaAsync(IdeaCreateDto ideaDto, int userId, bool trackChanges);
    }
}
