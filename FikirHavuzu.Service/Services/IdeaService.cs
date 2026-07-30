
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

        public IEnumerable<IdeaDto> GetAllIdeasWithDetails(IdeaRequestParameters p, bool trackChanges)
        {
            var ideas = _manager.Idea
                .GetAllIdeasWithDetails(p, trackChanges)
                .ToList();

            var ideaDtos = _mapper.Map<IEnumerable<IdeaDto>>(ideas);

            return ideaDtos;
        }

        public IEnumerable<IdeaDto> GetAllIdeas(bool trackChanges)
        {

            var ideas = _manager.Idea.FindAll(trackChanges);

            return _mapper.Map<IEnumerable<IdeaDto>>(ideas);
        }

        public int GetCount(IdeaRequestParameters p)
        {
            return _manager.Idea.GetCount(p);
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
    }
}
