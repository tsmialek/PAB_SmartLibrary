using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Application.Services.BookMenagement;
using SmartLibrary.Contracts.Book;

namespace SmartLibrary.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _bookService.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(Guid id)
        {
            var result = _bookService.GetBookById(id);
            var bookResponse = new BookResponse
            (
                result.Book.Id,
                result.Book.Title,
                result.Book.Author,
                result.Book.ISBN,
                result.Book.Description,
                result.Book.PageCount,
                result.Book.Date);

            return Ok(bookResponse); ;
        }

        [HttpGet("search")]
        public IActionResult GetBookByName(string name)
        {
            var result = _bookService.GetBookByName(name);

            var bookResponse = new BookResponse
            (
                result.Book.Id,
                result.Book.Title,
                result.Book.Author,
                result.Book.ISBN,
                result.Book.Description,
                result.Book.PageCount,
                result.Book.Date);

            return Ok(bookResponse);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookRequest request)
        {
            var bookResult = _bookService.AddBook(
                request.Title,
                request.Author,
                request.ISBN,
                request.Description,
                request.PageCount,
                request.Date);

            var bookResponse = new BookResponse
            (
                bookResult.Book.Id,
                bookResult.Book.Title,
                bookResult.Book.Author,
                bookResult.Book.ISBN,
                bookResult.Book.Description,
                bookResult.Book.PageCount,
                bookResult.Book.Date);

            return Ok(bookResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(Guid id)
        {
            var result = _bookService.DeleteBook(id);

            var bookResponse = new BookResponse
            (
                result.Book.Id,
                result.Book.Title,
                result.Book.Author,
                result.Book.ISBN,
                result.Book.Description,
                result.Book.PageCount,
                result.Book.Date);

            return Ok(bookResponse);
        }
    }
}
