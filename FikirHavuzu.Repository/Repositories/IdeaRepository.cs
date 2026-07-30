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

        // --- Temel CRUD İşlemleri ---
        public void CreateOneIdea(Idea idea) => Create(idea);
        public void DeleteOneIdea(Idea idea) => Remove(idea);
        public void UpdateOneIdea(Idea idea) => Update(idea);
        public IQueryable<Idea> GetAllIdeas(bool trackChanges) => FindAll(trackChanges);

        public IQueryable<Idea> GetAllIdeasWithDetails(IdeaRequestParameters p, bool trackChanges)
        {
            return FindAll(trackChanges)
                .Include(i => i.User)
                .Include(i => i.Category)
                .AsQueryable()
                .FilteredByCategoryId(p.CategoryId)
                .FilteredBySearchQuery(p.SearchQuery)
                .FilteredByFullName(p.FullName)
                .FilteredByDate(p.FilterDate)
                .OrderByDescending(i => i.Id)
                .ToPaginate(p.PageNumber, p.PageSize);
        }

        public Idea? GetOneIdea(int id, bool trackChanges)
        {
            return FindByCondition(i => i.Id.Equals(id), trackChanges)
                .Include(i => i.User)
                .Include(i => i.Category)
                .SingleOrDefault();
        }

        public int GetCount(IdeaRequestParameters p)
        {
            return _context.Ideas
                .Include(i => i.User)
                .Include(i => i.Category)
                .AsQueryable()
                .FilteredByCategoryId(p.CategoryId)
                .FilteredBySearchQuery(p.SearchQuery)
                .FilteredByFullName(p.FullName)
                .FilteredByDate(p.FilterDate)
                .Count();
        }
    }
}