
using AutoMapper;
using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Service.Contracts;

namespace FikirHavuzu.Service.Services
{
    public class IdeaService : IIdeaService
    {
        private readonly IRepositoryManager _manager;

        private readonly IMapper _mapper;

        public IdeaService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IdeaDto>> GetAllIdeasWithDetailsAsync(IdeaRequestParameters p, bool trackChanges)
        {
            var ideas = await _manager.Idea.GetAllIdeasWithDetailsAsync(p, trackChanges);

            return _mapper.Map<IEnumerable<IdeaDto>>(ideas);
        }

        public async Task<int> GetCountAsync(IdeaRequestParameters p)
        {
            return await _manager.Idea.GetCountAsync(p);
        }

        public async Task CreateIdeaAsync(IdeaCreateDto ideaDto, int userId, bool trackChanges)
        {
            var ideaEntity = _mapper.Map<Idea>(ideaDto);

            ideaEntity.UserId = userId;
            ideaEntity.CreatedAt = DateTime.Now;

            if (ideaDto.UploadedDocuments != null && ideaDto.UploadedDocuments.Any())
            {
                ideaEntity.Documents = new List<IdeaDocument>();

                foreach (var doc in ideaDto.UploadedDocuments)
                {
                    var document = new IdeaDocument
                    {
                        FileName = doc.FileName,
                        FilePath = doc.FilePath,
                        Idea = ideaEntity
                    };

                    ideaEntity.Documents.Add(document);
                }
            }

            _manager.Idea.Create(ideaEntity);

            await _manager.SaveAsync();
        }

        public async Task<IdeaDetailDto?> GetIdeaByIdWithDetailsAsync(int ideaId, bool trackChanges)
        {
            var idea = await _manager.Idea.GetIdeaByIdWithDetailsAsync(ideaId, trackChanges);

            if (idea is null)
            {
                return null;
            }

            return _mapper.Map<IdeaDetailDto>(idea);
        }
    }
}
