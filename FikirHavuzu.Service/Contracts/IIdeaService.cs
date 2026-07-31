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
        Task<IEnumerable<IdeaDto>> GetAllIdeasWithDetailsAsync(IdeaRequestParameters p, bool trackChanges);

        Task<int> GetCountAsync(IdeaRequestParameters p);

        Task CreateIdeaAsync(IdeaCreateDto ideaDto, int userId, bool trackChanges);
    }
}
