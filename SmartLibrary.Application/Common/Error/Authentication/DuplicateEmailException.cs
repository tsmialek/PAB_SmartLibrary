using System.Net;

namespace SmartLibrary.Application.Common.Error.Authentication
{
    public class DuplicateEmailException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "Email already exists.";
    }
}
