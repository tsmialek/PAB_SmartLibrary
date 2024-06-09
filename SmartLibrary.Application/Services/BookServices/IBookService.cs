using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Application.Services.BookMenagement
{
    public interface IBookService
    {
        Book AddBook(string title, string author, string isbn, string? description = null, int? pageCount = null, DateOnly? date = null);
        Book DeleteBook(Guid Id);
        Book GetBookById(Guid id);
        Book GetBookByName(string name);
        List<Domain.Entities.Book> GetBooks();
    }
}
