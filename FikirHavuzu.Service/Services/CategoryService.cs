
using AutoMapper;
using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Service.Contracts;

namespace FikirHavuzu.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _manager;

        private readonly IMapper _mapper;

        public CategoryService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetAllCategories(bool trackChanges)
        {

            var categories=_manager.Category.FindAll(trackChanges);

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
    }
}
