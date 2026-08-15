using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Repository.Repositories
{
    public class EvaluationRepository : RepositoryBase<Evaluation>, IEvaluationRepository
    {
        public EvaluationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Evaluation>> GetAllEvaluationsWithDetailsAsync(EvaluationRequestParameters p, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(e => e.EvaluatedByUser)
                .FilteredByIdeaId(p.IdeaId)
                .FilteredByScore(p.Score)
                .FilteredByIsApproved(p.IsApproved)
                .FilteredByComment(p.Comment)
                .FilteredByFullName(p.FullName)
                .FilteredByEmail(p.Email)
                .FilteredByDateRange(p.StartDate, p.EndDate)
                .OrderByDescending(e => e.EvaluationDate)
                .ToPaginate(p.PageNumber, p.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(EvaluationRequestParameters p)
        {
            return await FindAll(false)
                .FilteredByIdeaId(p.IdeaId)
                .FilteredByScore(p.Score)
                .FilteredByIsApproved(p.IsApproved)
                .FilteredByComment(p.Comment)
                .FilteredByFullName(p.FullName)
                .FilteredByEmail(p.Email)
                .FilteredByDateRange(p.StartDate, p.EndDate)
                .CountAsync();
        }
    }
}