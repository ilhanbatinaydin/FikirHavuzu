using FikirHavuzu.Entity.Dtos.Idea;

namespace FikirHavuzu.Service.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories(bool trackChanges);
    }
}
