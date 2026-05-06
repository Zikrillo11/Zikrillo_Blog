using Microsoft.AspNetCore.Mvc;
using Zikrillo_Blog.BLL.Interfaces;
using Zikrillo_Blog.Shared.DTOs.Post; // DTO namespace'i
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Zikrillo_Blog.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPostService _postService;

        public AdminController(IPostService postService)
        {
            _postService = postService;
        }

        // Dashboard: hamma postlarni ko'rish
        public async Task<IActionResult> Index()
        {
            // GetAllAsync() PostForResultDto ro'yxatini qaytaradi
            var posts = await _postService.GetAllAsync();
            return View(posts.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(PostForCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                // 1. CategoryId Guid bo'lishi shart. Agar empty bo'lsa, biror ID beramiz
                if (dto.CategoryId == Guid.Empty)
                {
                    // BU YERGA: Bazangizdagi birorta Kategoriya ID-sini qo'ying
                    dto.CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000000");
                }

                // 2. UserId talab qilingani uchun statik ID yaratamiz
                // BU YERGA: Bazangizdagi (User jadvalidagi) Admin ID-sini qo'ying[cite: 1]
                Guid currentUserId = Guid.Parse("00000000-0000-0000-0000-000000000000");

                // 3. Metodni ikkala parametr bilan chaqiramiz[cite: 1]
                await _postService.CreateAsync(dto, currentUserId);

                return RedirectToAction(nameof(Index));
            }

            // Agar validatsiya xatosi bo'lsa, yana Indexga
            return RedirectToAction(nameof(Index));
        }
    }
} 