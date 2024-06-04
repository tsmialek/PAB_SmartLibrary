using System.Net;

namespace SmartLibrary.Application.Common.Error.Authentication
{
    public class InvalidCredentialsException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage => "Invalid login credentials";
    }
}
