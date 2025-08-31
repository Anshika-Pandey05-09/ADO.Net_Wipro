using Day34_LibraryManagementSystem.Models;
using Day34_LibraryManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IUnitOfWork _uow;
        public AuthorsController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index()
        {
            var items = await _uow.Authors.GetAllAsync(orderBy:q=>q.OrderBy(a=>a.Name));
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_List", items);
            return View(items);
        }

        [HttpGet] public IActionResult Create() => PartialView("_CreateOrEdit", new Author());

        [HttpPost]
        public async Task<IActionResult> Create(Author model)
        {
            if (!ModelState.IsValid) return PartialView("_CreateOrEdit", model);
            await _uow.Authors.AddAsync(model);
            await _uow.SaveAsync();
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var a = await _uow.Authors.GetByIdAsync(id);
            if (a == null) return NotFound();
            return PartialView("_CreateOrEdit", a);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author model)
        {
            if (!ModelState.IsValid) return PartialView("_CreateOrEdit", model);
            _uow.Authors.Update(model);
            await _uow.SaveAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _uow.Authors.DeleteAsync(id);
            await _uow.SaveAsync();
            return Json(new { success = true });
        }
    }
}