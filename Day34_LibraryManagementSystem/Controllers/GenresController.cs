using Day34_LibraryManagementSystem.Models;
using Day34_LibraryManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Day34_LibraryManagementSystem.Controllers
{
    public class GenresController : Controller
    {
        private readonly IUnitOfWork _uow;
        public GenresController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index()
        {
            var items = await _uow.Genres.GetAllAsync(orderBy:q=>q.OrderBy(g=>g.Name));
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_List", items);
            return View(items);
        }

        [HttpGet] public IActionResult Create() => PartialView("_CreateOrEdit", new Genre());

        [HttpPost]
        public async Task<IActionResult> Create(Genre model)
        {
            if (!ModelState.IsValid) return PartialView("_CreateOrEdit", model);
            await _uow.Genres.AddAsync(model);
            await _uow.SaveAsync();
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var g = await _uow.Genres.GetByIdAsync(id);
            if (g == null) return NotFound();
            return PartialView("_CreateOrEdit", g);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Genre model)
        {
            if (!ModelState.IsValid) return PartialView("_CreateOrEdit", model);
            _uow.Genres.Update(model);
            await _uow.SaveAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _uow.Genres.DeleteAsync(id);
            await _uow.SaveAsync();
            return Json(new { success = true });
        }
    }
}