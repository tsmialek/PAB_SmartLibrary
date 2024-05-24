using SmartLibrary.Application.Common.Interfaces.Persistance;
using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Infrastructure.Persistance
{
    public class RoleRepository : IRoleRepository
    {
        private static readonly List<Role> _roles = new();

        public IEnumerable<Role> GetAll()
        {
            return _roles;
        }

        public Role GetById(Guid id)
        {
            return _roles.SingleOrDefault(r => r.Id == id);
        }

        public Role GetByName(string name)
        {
            return _roles.SingleOrDefault(r => r.Name == name);
        }

        public void Add(Role role)
        {
            _roles.Add(role);
        }

        public void Update(Role role)
        {
            _roles[_roles.IndexOf(role)] = role;
        }

        public void Delete(Guid id)
        {
            var role = GetById(id);
            _roles.Remove(role);
        }
    }
}
