using AutoMapper;
using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Service.Exceptions;
using FikirHavuzu.Web.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FikirHavuzu.Web.Controllers
{
    public class EvaluationController : Controller
    {
        private readonly IServiceManager _manager;

        private readonly IMapper _mapper;

        public EvaluationController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IdeaEvaluatePolicy")]
        public async Task<IActionResult> Create([FromForm] EvaluationCreateViewModel newEvaluation, [FromServices] IValidator<EvaluationCreateViewModel> validator)
        {
            var validationResult = await validator.ValidateAsync(newEvaluation);

            if (!validationResult.IsValid)
            {
                TempData["ErrorMessage"] = validationResult.Errors.First().ErrorMessage;
                return RedirectToAction("Detail", "Idea", new { id = newEvaluation.IdeaId });
            }

            try
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var evaluationDto = _mapper.Map<EvaluationCreateDto>(newEvaluation);

                await _manager.EvaluationService.CreateEvaluationAsync(evaluationDto, userId);

                TempData["SuccessMessage"] = "Değerlendirmeniz başarıyla sisteme eklenmiştir!";
                return RedirectToAction("Detail", "Idea", new { id = newEvaluation.IdeaId });
            }
            catch (NotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Detail", "Idea", new { id = newEvaluation.IdeaId });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Değerlendirmeniz kaydedilirken bir sistem hatası oluştu.";
                return RedirectToAction("Detail", "Idea", new { id = newEvaluation.IdeaId });
            }
        }
    }
}
