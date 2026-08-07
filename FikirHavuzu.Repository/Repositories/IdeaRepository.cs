using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Repository.Repositories
{
    public class IdeaRepository : RepositoryBase<Idea>, IIdeaRepository
    {
        public IdeaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Idea>> GetAllIdeasWithDetailsAsync(IdeaRequestParameters p, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(i => i.User)
                .Include(i => i.Category)
                .FilteredByCategoryId(p.CategoryId)
                .FilteredBySearchQuery(p.SearchQuery)
                .FilteredByFullName(p.FullName)
                .FilteredByDateRange(p.StartDate, p.EndDate)
                .OrderByDescending(i => i.Id)
                .ToPaginate(p.PageNumber, p.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(IdeaRequestParameters p)
        {
            return await FindAll(false)
                .FilteredByCategoryId(p.CategoryId)
                .FilteredBySearchQuery(p.SearchQuery)
                .FilteredByFullName(p.FullName)
                .FilteredByDateRange(p.StartDate, p.EndDate)
                .CountAsync();
        }

        public async Task<Idea> GetIdeaByIdWithDetailsAsync(int ideaId, bool trackChanges)
        {
            return await FindByCondition(i => i.Id == ideaId, trackChanges)
                        .Include(i => i.User)
                        .Include(i => i.Category)
                        .Include(i=>i.Documents)
                        .SingleOrDefaultAsync();
        }
    }
}