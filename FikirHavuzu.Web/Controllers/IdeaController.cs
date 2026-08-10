using AutoMapper;
using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.RequestParameters;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Service.Exceptions;
using FikirHavuzu.Web.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FikirHavuzu.Controllers
{
    public class IdeaController : Controller
    {
        private readonly IServiceManager _manager;

        private readonly IMapper _mapper;

        public IdeaController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "IdeaCreatePolicy")]
        public async Task<IActionResult> Create()
        {
            var categories = await _manager.CategoryService.GetAllCategoriesAsync(false);
            var viewModel = new IdeaCreateViewModel
            {
                CategoryList = new SelectList(categories, "Id", "Name")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IdeaCreatePolicy")]
        public async Task<IActionResult> Create([FromForm] IdeaCreateViewModel model, [FromServices] IValidator<IdeaCreateViewModel> validator)
        {
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                var categories = await _manager.CategoryService.GetAllCategoriesAsync(false);
                model.CategoryList = new SelectList(categories, "Id", "Name");
                return View(model);
            }

            try
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var ideaDto = _mapper.Map<IdeaCreateDto>(model);

                if (model.Documents != null && model.Documents.Count > 0)
                {
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "ideas");
                    if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                    foreach (var file in model.Documents)
                    {
                        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                        var safeFileName = $"{Guid.NewGuid()}{extension}";
                        var filePath = Path.Combine(uploadPath, safeFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        ideaDto.UploadedDocuments.Add(new UploadedDocumentDto
                        {
                            FileName = file.FileName,
                            FilePath = $"/uploads/ideas/{safeFileName}"
                        });
                    }
                }

                await _manager.IdeaService.CreateIdeaAsync(ideaDto, userId);
                TempData["SuccessMessage"] = "Fikriniz başarıyla sisteme eklenmiştir!";
                return RedirectToAction("Index", "Home");
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var categories = await _manager.CategoryService.GetAllCategoriesAsync(false);
                model.CategoryList = new SelectList(categories, "Id", "Name");

                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Fikriniz kaydedilirken bir sistem hatası oluştu.");
                var categories = await _manager.CategoryService.GetAllCategoriesAsync(false);
                model.CategoryList = new SelectList(categories, "Id", "Name");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Policy = "IdeaViewPolicy")]
        public async Task<IActionResult> Detail(int id, EvaluationRequestParameters evalParams)
        {
            if (Request.Headers.ContainsKey("HX-Request"))
            {
                evalParams.IdeaId = id;
                return ViewComponent("EvaluationList", new { parameters = evalParams });
            }


            try
            {
                var idea = await _manager.IdeaService.GetIdeaByIdWithDetailsAsync(id, trackChanges: false);

                var model = new IdeaDetailViewModel
                {
                    Idea = idea
                };

                return View(model);
            }
            catch (NotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Fikir detayları yüklenirken sunucu kaynaklı bir hata oluştu.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}