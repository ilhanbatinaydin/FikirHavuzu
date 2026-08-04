using AutoMapper;
using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Service.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public EvaluationService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EvaluationDto>> GetAllEvaluationsWithDetailsAsync(EvaluationRequestParameters p, bool trackChanges)
        {
            var evaluations = await _manager.Evaluation.GetAllEvaluationsWithDetailsAsync(p, trackChanges);

            return _mapper.Map<IEnumerable<EvaluationDto>>(evaluations);
        }

        public async Task<int> GetCountAsync(EvaluationRequestParameters p)
        {
            return await _manager.Evaluation.GetCountAsync(p);
        }

        public async Task CreateEvaluationAsync(EvaluationCreateDto evaluationDto, int userId, bool trackChanges)
        {
            var ideaExists = await _manager.Idea.FindByCondition(i => i.Id == evaluationDto.IdeaId, false).AnyAsync();

            if (!ideaExists)
            {
                throw new NotFoundException("Değerlendirme yapılmak istenen fikir sistemde bulunamadı.");
            }

            var evaluationEntity = _mapper.Map<Evaluation>(evaluationDto);

            evaluationEntity.EvaluatedByUserId = userId;
            evaluationEntity.EvaluationDate = DateTime.Now;

            _manager.Evaluation.Create(evaluationEntity);
            await _manager.SaveAsync();
        }
    }
}