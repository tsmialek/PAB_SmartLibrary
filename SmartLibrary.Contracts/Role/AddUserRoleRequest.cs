using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Contracts.Role
{
    public class AddUserRoleByEmailAndNameRequest
    {
        public string UserEmail { get; set; } = null!;
        public string NewUserRoleName { get; set; } = null!;
    }

    public class  AddUserRoleByIdRequest 
    {
        public Guid UserId { get; set; }
        public Guid NewUserRoleId { get; set; }
    }
}
