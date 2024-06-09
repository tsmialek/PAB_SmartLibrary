using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Application.Services.BookMenagement;
using SmartLibrary.Contracts.Book;

namespace SmartLibrary.API.Controllers
{
    [Route("books")]
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
                result.Id,
                result.Title,
                result.Author,
                result.ISBN,
                result.Description,
                result.PageCount,
                result.Date);

            return Ok(bookResponse); ;
        }

        [HttpGet("search")]
        public IActionResult GetBookByName(string name)
        {
            var result = _bookService.GetBookByName(name);

            var bookResponse = new BookResponse
            (
                result.Id,
                result.Title,
                result.Author,
                result.ISBN,
                result.Description,
                result.PageCount,
                result.Date);

            return Ok(bookResponse);
        }

        [Authorize(Roles = "Admin")]
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
                bookResult.Id,
                bookResult.Title,
                bookResult.Author,
                bookResult.ISBN,
                bookResult.Description,
                bookResult.PageCount,
                bookResult.Date);

            return Ok(bookResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(Guid id)
        {
            var result = _bookService.DeleteBook(id);

            var bookResponse = new BookResponse
            (
                result.Id,
                result.Title,
                result.Author,
                result.ISBN,
                result.Description,
                result.PageCount,
                result.Date);

            return Ok(bookResponse);
        }
    }
}
