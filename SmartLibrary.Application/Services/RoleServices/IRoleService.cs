using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Application.Services.RoleServices
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetById(Guid id);
        Role GetByName(string name);
        User AddUserRole(Guid userId, Guid roleId);
        User AddUserRole(string userEmail, string roleName);
    }
}
