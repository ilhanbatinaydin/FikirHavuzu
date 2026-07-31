using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Repository.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
        {
            var query = trackChanges ? _context.Categories : _context.Categories.AsNoTracking();

            return await query.OrderBy(c => c.Name).ToListAsync();
        }
    }
}