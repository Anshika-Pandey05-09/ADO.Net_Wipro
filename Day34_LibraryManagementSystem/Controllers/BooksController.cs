using Day34_LibraryManagementSystem.Models;
using Day34_LibraryManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Day34_LibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly IUnitOfWork _uow;
        private const int PageSize = 8;

        public BooksController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index(string? search, string? sort, int page = 1)
        {
            var (items, total) = await _uow.Books.SearchAsync(search, sort, page, PageSize);
            ViewBag.Search = search;
            ViewBag.Sort = sort;
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)PageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_List", items);

            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _uow.Books.GetDetailsAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _uow.Authors.GetAllAsync(orderBy: q => q.OrderBy(a => a.Name));
            ViewBag.Genres = await _uow.Genres.GetAllAsync(orderBy: q => q.OrderBy(g => g.Name));
            return PartialView("_CreateOrEdit", new Book());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book model, int[] genreIds)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateOrEdit", model);

            try
            {
                await _uow.Books.AddAsync(model);
                await _uow.SaveAsync();
                await _uow.Books.UpdateGenresAsync(model, genreIds);
                await _uow.SaveAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { success = false, error = "Failed to create book: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _uow.Books.GetByIdAsync(id);
            if (book == null) return NotFound();
            ViewBag.Authors = await _uow.Authors.GetAllAsync(orderBy: q => q.OrderBy(a => a.Name));
            ViewBag.Genres = await _uow.Genres.GetAllAsync(orderBy: q => q.OrderBy(g => g.Name));
            return PartialView("_CreateOrEdit", book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book model, int[] genreIds)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateOrEdit", model);

            try
            {
                _uow.Books.Update(model);
                await _uow.SaveAsync();
                await _uow.Books.UpdateGenresAsync(model, genreIds);
                await _uow.SaveAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { success = false, error = "Failed to update book: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _uow.Books.DeleteAsync(id);
                await _uow.SaveAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { success = false, error = "Failed to delete book: " + ex.Message });
            }
        }
    }
}