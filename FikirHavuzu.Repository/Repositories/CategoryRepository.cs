using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;

namespace FikirHavuzu.Repository.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }
    }
}