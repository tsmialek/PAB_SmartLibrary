using SmartLibrary.Application.Common.Error.Authentication;
using SmartLibrary.Application.Common.Error.Role;
using SmartLibrary.Application.Common.Interfaces.Persistance;
using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Application.Services.RoleServices
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleRepository.GetAll().ToList();
        }

        public Role GetById(Guid id)
        {
            if (_roleRepository.GetById(id) is not Role role)
            {
                throw new NonExistingRoleException();
            }

            return role;
        }

        public Role GetByName(string name)
        {
            if (_roleRepository.GetByName(name) is not Role role)
            {
                throw new NonExistingRoleException();
            }

            return role;
        }

        public User AddUserRole(Guid userId, Guid roleId)
        {
            if (_userRepository.GetById(userId) is not User user)
            {
                throw new NonExistingUserException();
            }

            if (_roleRepository.GetById(roleId) is not Role role)
            {
                throw new NonExistingRoleException();
            }

            user.Roles.Add(role);
            _userRepository.Update(user);
            return user;
        }

        public User AddUserRole(string userEmail, string roleName)
        {
            if (_userRepository.GetByEmail(userEmail) is not User user)
            {
                throw new NonExistingUserException();
            }

            if (_roleRepository.GetByName(roleName) is not Role role)
            {
                throw new NonExistingRoleException();
            }

            user.Roles.Add(role);
            role.Users.Add(user);

            _userRepository.Update(user);
            _roleRepository.Update(role);

            return user;
        }
    }
}
