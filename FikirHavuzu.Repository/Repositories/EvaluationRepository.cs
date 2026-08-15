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
                .Include(e => e.Idea)
                    .ThenInclude(i => i.Category)
                .FilteredByIdeaId(p.IdeaId)
                .FilteredByUserId(p.UserId)
                .FilteredByCategoryId(p.CategoryId)
                .FilteredBySearchQuery(p.SearchQuery)
                .FilteredByFullName(p.FullName)
                .FilteredByEmail(p.Email)
                .FilteredByComment(p.Comment)
                .FilteredByScore(p.Score)
                .FilteredByApprovalStatus(p.IsApproved)
                .FilteredByDateRange(p.StartDate, p.EndDate)
                .OrderByDescending(e => e.EvaluationDate)
                .ToPaginate(p.PageNumber, p.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(EvaluationRequestParameters p)
        {
            return await FindAll(false)
                .FilteredByIdeaId(p.IdeaId)
                .FilteredByUserId(p.UserId)
                .FilteredByCategoryId(p.CategoryId)
                .FilteredBySearchQuery(p.SearchQuery)
                .FilteredByFullName(p.FullName)
                .FilteredByEmail(p.Email)
                .FilteredByComment(p.Comment)
                .FilteredByScore(p.Score)
                .FilteredByApprovalStatus(p.IsApproved)
                .FilteredByDateRange(p.StartDate, p.EndDate)
                .CountAsync();
        }
    }
}