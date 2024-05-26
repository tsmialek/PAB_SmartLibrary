using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Application.Common.Interfaces.Persistance
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}
