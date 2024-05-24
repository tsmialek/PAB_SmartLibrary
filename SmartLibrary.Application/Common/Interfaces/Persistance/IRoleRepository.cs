using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Application.Common.Interfaces.Persistance
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role GetById(Guid id);
        Role GetByName(string name);
        void Add(Role role);
        void Update(Role role);
        void Delete(Guid id);
    }
}
