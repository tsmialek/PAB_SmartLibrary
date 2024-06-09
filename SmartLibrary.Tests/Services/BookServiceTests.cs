using FluentAssertions;
using Moq;
using SmartLibrary.Application.Common.Error.Book;
using SmartLibrary.Application.Common.Interfaces.Persistance;
using SmartLibrary.Application.Services.BookMenagement;
using SmartLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Tests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookService = new BookService(_bookRepositoryMock.Object);
        }

        [Fact]
        public void ListBook_ShouldReturnListOfBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = Guid.NewGuid(), Title = "Book1", Author = "Author1", ISBN = "ISBN1" },
                new Book { Id = Guid.NewGuid(), Title = "Book2", Author = "Author2", ISBN = "ISBN2" },
                new Book { Id = Guid.NewGuid(), Title = "Book3", Author = "Author3", ISBN = "ISBN3" }
            };
            _bookRepositoryMock.Setup(repo => repo.GetAll()).Returns(books);

            // Act 
            var result = _bookService.GetBooks();

            // Assert 
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(books);
        }

        [Fact]
        public void GetBookById_WithExistingId_ShouldReturnBook()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new Book { Id = bookId, Author = "Author", Title = "Title", ISBN = "ISBN" };
            _bookRepositoryMock.Setup(repo => repo.GetById(bookId)).Returns(book);

            // Act 
            var result = _bookService.GetBookById(bookId);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(book);
        }

        [Fact]
        public void GetBookById_WithNonExistingId_ShouldThrowBookNotFoundException()
        {
            // Arrange
            var nonExistingBookId = Guid.NewGuid();
            var book = new Book { Id = Guid.NewGuid(), Author = "Author", Title = "Title", ISBN = "ISBN" };
            _bookRepositoryMock.Setup(repo => repo.GetById(nonExistingBookId)).Returns((Book)null);

            // Act
            var result = () => _bookService.GetBookById(nonExistingBookId);

            // Assert
            result.Should().Throw<BookNotFoundException>();
        }

        [Fact]
        public void GetBookByName_WithExistingName_ShouldReturnBook()
        {
            // Arrange
            var bookName = "existingBookName";
            var book = new Book { Author = "Author", Title = bookName, ISBN = "ISBN" };
            _bookRepositoryMock.Setup(repo => repo.GetByName(bookName)).Returns(book);

            // Act 
            var result = _bookService.GetBookByName(bookName);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(book);
        }

        [Fact]
        public void GetBookByName_WithNonExistingName_ShouldThrowBookNotFoundException()
        {
            // Arrange
            var nonExistingBookName = "nonExistingBookName";
            var book = new Book { Author = "Author", Title = "existingBookName", ISBN = "ISBN" };
            _bookRepositoryMock.Setup(repo => repo.GetByName(nonExistingBookName)).Returns((Book)null);

            // Act
            var result = () => _bookService.GetBookByName(nonExistingBookName);

            // Assert
            result.Should().Throw<BookNotFoundException>();
        }

        [Fact]
        public void AddBook_WithValidBook_ShouldReturnAddedBookResult()
        {
            // Arrange
            var book = new Book { Author = "Author", Title = "Title", ISBN = "ISBN" };
            _bookRepositoryMock.Setup(repo => repo.Add(It.IsAny<Book>())).Callback<Book>(b => b.Id = Guid.NewGuid());

            // Act 
            var result = _bookService.AddBook(book.Title, book.Author, book.ISBN);

            // Assert
            result.Should().NotBeNull();
            result.Should().NotBeNull();
            result.Id.Should().NotBe(Guid.Empty);
            result.Title.Should().Be(book.Title);
            result.Author.Should().Be(book.Author);
            result.ISBN.Should().Be(book.ISBN);
        }

        [Fact]
        public void DeleteBook_WithValidId_ShouldReturnDeletedBookResult()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new Book { Id = bookId, Title = "Test Book", Author = "Author", ISBN = "ISBN" };
            _bookRepositoryMock.Setup(repo => repo.GetById(bookId)).Returns(book);
            _bookRepositoryMock.Setup(repo => repo.Delete(bookId));

            // Act
            var result = _bookService.DeleteBook(bookId);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(book);
        }

        [Fact]
        public void DeleteBook_WithInvalidId_ShouldThrowBookNotFoundException()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            _bookRepositoryMock.Setup(repo => repo.GetById(bookId)).Returns((Book)null);

            // Act
            var act = () => _bookService.DeleteBook(bookId);

            // Assert
            act.Should().Throw<BookNotFoundException>();
        }
    }
}
