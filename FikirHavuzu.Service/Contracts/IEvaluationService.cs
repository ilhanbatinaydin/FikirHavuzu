using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.RequestParameters;

namespace FikirHavuzu.Service.Contracts
{
    public interface IEvaluationService
    {
        Task<IEnumerable<EvaluationDto>> GetAllEvaluationsWithDetailsAsync(EvaluationRequestParameters p, bool trackChanges);

        Task<int> GetCountAsync(EvaluationRequestParameters p);

        Task CreateEvaluationAsync(EvaluationCreateDto evaluationDto, int userId);
    }
}
