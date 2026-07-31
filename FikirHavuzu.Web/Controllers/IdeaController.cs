using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FikirHavuzu.Controllers
{
    [Authorize(Policy = "IdeaCreatePolicy")]
    public class IdeaController : Controller
    {
        private readonly IServiceManager _manager;

        public IdeaController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Kategori okuma işlemi I/O'dur, asenkron olmalı.
            var categories = await _manager.CategoryService.GetAllCategoriesAsync(false);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] IdeaCreateDto ideaDto, List<IFormFile> documents)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _manager.CategoryService.GetAllCategoriesAsync(false), "Id", "Name");
                return View(ideaDto);
            }

            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                {
                    return RedirectToAction("Login", "Auth");
                }

                if (documents != null && documents.Count > 0)
                {
                    var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".png", ".jpg", ".jpeg" };
                    long maxFileSize = 5 * 1024 * 1024;

                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "ideas");

                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    foreach (var file in documents)
                    {
                        if (file.Length > maxFileSize)
                        {
                            ModelState.AddModelError("", $"'{file.FileName}' adlı dosya 5MB boyutunu aşıyor.");
                            ViewBag.Categories = new SelectList(await _manager.CategoryService.GetAllCategoriesAsync(false), "Id", "Name");
                            return View(ideaDto);
                        }

                        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                        if (!allowedExtensions.Contains(extension))
                        {
                            ModelState.AddModelError("", $"'{file.FileName}' desteklenmeyen bir dosya formatı.");
                            ViewBag.Categories = new SelectList(await _manager.CategoryService.GetAllCategoriesAsync(false), "Id", "Name");
                            return View(ideaDto);
                        }

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

                await _manager.IdeaService.CreateIdeaAsync(ideaDto, userId, false);

                TempData["SuccessMessage"] = "Fikriniz başarıyla sisteme eklenmiştir!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Fikriniz kaydedilirken bir sistem hatası oluştu.");
                ViewBag.Categories = new SelectList(await _manager.CategoryService.GetAllCategoriesAsync(false), "Id", "Name");
                return View(ideaDto);
            }
        }
    }
}