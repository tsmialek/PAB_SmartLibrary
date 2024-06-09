using SmartLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetByEmail(string email);
        User GetByName(string name);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        void Update(User user);
        void Delete(Guid id);
    }
}
