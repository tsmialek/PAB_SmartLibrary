using SmartLibrary.Application.Common.Interfaces.Persistance;
using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Infrastructure.Persistance
{
    public class BookRepository: IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book GetById(Guid id)
        {
            return _context.Books.Find(id);
        }

        public Book GetByName(string name)
        {
            return _context.Books.FirstOrDefault(x => x.Title == name);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
