using Microsoft.EntityFrameworkCore;
using SmartLibrary.Application.Common.Interfaces.Persistance;
using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Infrastructure.Persistance
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles.Include(r => r.Users).ToList();
        }

        public Role GetById(Guid id)
        {
            return _context.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == id);
        }

        public Role GetByName(string name)
        {
            return _context.Roles.Include(r => r.Users).SingleOrDefault(r => r.Name == name);
        }

        public void Add(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }
    }
}
