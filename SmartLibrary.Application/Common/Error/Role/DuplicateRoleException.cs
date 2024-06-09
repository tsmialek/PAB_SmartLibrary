using System.Net;

namespace SmartLibrary.Application.Common.Error.Role
{
    public class DuplicateRoleException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "Role with given name already exists";
    }
}
