using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;

namespace FikirHavuzu.Repository.Contracts
{
    public interface IEvaluationRepository : IRepositoryBase<Evaluation>
    {
        Task<IEnumerable<Evaluation>> GetAllEvaluationsWithDetailsAsync(EvaluationRequestParameters p, bool trackChanges);

        Task<int> GetCountAsync(EvaluationRequestParameters p);
    }
}