using Infrastructure.Services.Books;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class BooksController : Controller
    {
        public IBooksService _booksService { get; set; }

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _booksService.GetBooks();
            return View(books.Item2);
        }
    }
}
