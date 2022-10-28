using System.Net;

namespace HospitalAPI.ErrorHandling.Exceptions
{
    public class CustomBadRequestException: BaseException
    {
        public CustomBadRequestException(string message) : base(message,(int)HttpStatusCode.BadRequest)
        {
        }
    }
}