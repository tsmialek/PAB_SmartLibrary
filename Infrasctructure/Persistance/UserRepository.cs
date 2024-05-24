using Microsoft.EntityFrameworkCore;
using SmartLibrary.Application.Common.Interfaces.Persistance;
using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Infrastructure.Persistance
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.Include(u => u.Roles).SingleOrDefault(u => u.Email == email);
        }

        public User GetByName(string name)
        {
            return _context.Users.Include(u => u.Roles).SingleOrDefault(u => u.FirstName == name || u.LastName == name);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(u => u.Roles).ToList();
        }

        public User GetById(Guid id)
        {
            return _context.Users.Include(u => u.Roles).SingleOrDefault(u => u.Id == id);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
