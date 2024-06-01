using System.Net;

namespace SmartLibrary.Application.Common.Error
{
    public interface IServiceException 
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}
