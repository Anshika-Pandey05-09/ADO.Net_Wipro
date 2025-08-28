using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Day33_LibraryManagement_assignment.Data;
using Day33_LibraryManagement_assignment.Models;

namespace Day33_LibraryManagement_assignment.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.Include(b => b.Author).ToListAsync();
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null) return NotFound();

            return View(book);
        }

        // // GET: Books/Create
        // public IActionResult Create()
        // {
        //     ViewBag.Authors = _context.Authors.ToList();
        //     return View();
        // }

        // // POST: Books/Create
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create(Book book)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(book);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(book);
        // }

        // // GET: Books/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null) return NotFound();

        //     var book = await _context.Books.FindAsync(id);
        //     if (book == null) return NotFound();

        //     ViewBag.Authors = _context.Authors.ToList();
        //     return View(book);
        // }

        // // POST: Books/Edit/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, Book book)
        // {
        //     if (id != book.BookId) return NotFound();

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(book);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!_context.Books.Any(e => e.BookId == book.BookId))
        //                 return NotFound();
        //             else
        //                 throw;
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(book);
        // }

        // GET: Books/Create
public IActionResult Create()
{
    ViewBag.Authors = _context.Authors.ToList();
    ViewBag.Genres = _context.Genres.ToList();
    return View();
}

// POST: Books/Create
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Book book, int[] selectedGenres)
{
    if (ModelState.IsValid)
    {
        // Attach genres
        foreach (var genreId in selectedGenres)
        {
            var genre = await _context.Genres.FindAsync(genreId);
            if (genre != null)
            {
                book.Genres.Add(genre);
            }
        }

        _context.Add(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    ViewBag.Authors = _context.Authors.ToList();
    ViewBag.Genres = _context.Genres.ToList();
    return View(book);
}

// GET: Books/Edit/5
public async Task<IActionResult> Edit(int? id)
{
    if (id == null) return NotFound();

    var book = await _context.Books
        .Include(b => b.Genres)
        .FirstOrDefaultAsync(b => b.BookId == id);

    if (book == null) return NotFound();

    ViewBag.Authors = _context.Authors.ToList();
    ViewBag.Genres = _context.Genres.ToList();
    ViewBag.SelectedGenres = book.Genres.Select(g => g.GenreId).ToList();

    return View(book);
}

// POST: Books/Edit/5
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, Book book, int[] selectedGenres)
{
    if (id != book.BookId) return NotFound();

    if (ModelState.IsValid)
    {
        try
        {
            var existingBook = await _context.Books
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (existingBook == null) return NotFound();

            // Update scalar properties
            existingBook.Title = book.Title;
            existingBook.AuthorId = book.AuthorId;

            // Update genres
            existingBook.Genres.Clear();
            foreach (var genreId in selectedGenres)
            {
                var genre = await _context.Genres.FindAsync(genreId);
                if (genre != null)
                {
                    existingBook.Genres.Add(genre);
                }
            }

            _context.Update(existingBook);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Books.Any(e => e.BookId == book.BookId))
                return NotFound();
            else
                throw;
        }
        return RedirectToAction(nameof(Index));
    }
    ViewBag.Authors = _context.Authors.ToList();
    ViewBag.Genres = _context.Genres.ToList();
    return View(book);
}


        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}