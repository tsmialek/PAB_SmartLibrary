using System.Net;

namespace SmartLibrary.Application.Common.Error
{
    public class NonExistingUserException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "User with given email does not exist.";
    }
}
