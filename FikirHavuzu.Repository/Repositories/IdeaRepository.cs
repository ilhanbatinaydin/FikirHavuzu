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
            var query = trackChanges ? _context.Ideas : _context.Ideas.AsNoTracking();

            return await query
                .Include(i => i.User)
                .Include(i => i.Category)
                .AsQueryable()
                .FilteredByCategoryId(p.CategoryId)
                .FilteredBySearchQuery(p.SearchQuery)
                .FilteredByFullName(p.FullName)
                .FilteredByDate(p.FilterDate)
                .OrderByDescending(i => i.Id)
                .ToPaginate(p.PageNumber, p.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(IdeaRequestParameters p)
        {
            return await _context.Ideas.AsNoTracking()
                .Include(i => i.User)
                .Include(i => i.Category)
                .AsQueryable()
                .FilteredByCategoryId(p.CategoryId)
                .FilteredBySearchQuery(p.SearchQuery)
                .FilteredByFullName(p.FullName)
                .FilteredByDate(p.FilterDate)
                .CountAsync();
        }
    }
}