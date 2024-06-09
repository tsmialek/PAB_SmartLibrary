using SmartLibrary.Application.Common.Error.Book;
using SmartLibrary.Application.Common.Interfaces.Persistance;
using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Application.Services.BookMenagement
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book AddBook(string title, string author, string isbn, string? description = null, int? pageCount = null, DateOnly? date = null)
        {
            var book = new Book()
            {
                Title = title,
                Author = author,
                ISBN = isbn,
                Description = description,
                PageCount = pageCount,
                Date = date
            };

            _bookRepository.Add(book);

            return book;
        }

        public Book DeleteBook(Guid Id)
        {
            if (_bookRepository.GetById(Id) is not Book book)
            {
                throw new BookNotFoundException();
            }

            _bookRepository.Delete(Id);

            return book;
        }

        public Book GetBookById(Guid id)
        {
            if (_bookRepository.GetById(id) is not Book book)
            {
                throw new BookNotFoundException();
            }

            return book;
        }

        public List<Book> GetBooks()
        {
            var books = _bookRepository.GetAll().ToList();

            return books;
        }

        public Book GetBookByName(string name)
        {
            if (_bookRepository.GetByName(name) is not Book book)
            {
                throw new BookNotFoundException();
            }

            return book;
        }
    }
}
