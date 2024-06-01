using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Application.Services.BookMenagement
{
    public interface IBookService
    {
        BookResult AddBook(string title, string author, string isbn, string? description = null, int? pageCount = null, DateOnly? date = null);
        BookResult DeleteBook(Guid Id);
        BookResult GetBookById(Guid id);
        BookResult GetBookByName(string name);
        List<Domain.Entities.Book> GetBooks();
    }
}
