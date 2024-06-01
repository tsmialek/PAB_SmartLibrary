using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Application.Common.Interfaces.Persistance
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(Guid id);
        Book GetByName(string name);
        void Add(Book book);
        void Update(Book book);
        void Delete(Guid id);
    }
}
