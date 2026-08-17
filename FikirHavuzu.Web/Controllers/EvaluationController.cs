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
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return PartialView("_EvaluationCreatePartial", newEvaluation);
            }

            try
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var evaluationDto = _mapper.Map<EvaluationCreateDto>(newEvaluation);

                await _manager.EvaluationService.CreateEvaluationAsync(evaluationDto, userId);

                Response.Headers.Append("HX-Trigger", "refreshEvaluations");

                ModelState.Clear();

                var cleanModel = new EvaluationCreateViewModel
                {
                    IdeaId = newEvaluation.IdeaId,
                    IsApproved = true
                };

                ViewBag.SuccessMessage = "Değerlendirmeniz başarıyla sisteme eklendi!";
                return PartialView("_EvaluationCreatePartial", cleanModel);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView("_EvaluationCreatePartial", newEvaluation);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Değerlendirmeniz kaydedilirken sistemsel bir hata oluştu.");
                return PartialView("_EvaluationCreatePartial", newEvaluation);
            }
        }
    }
}
