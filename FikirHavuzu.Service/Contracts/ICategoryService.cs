using FikirHavuzu.Entity.Dtos.Idea;

namespace FikirHavuzu.Service.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges);
    }
}
