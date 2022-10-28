using System.Net;

namespace HospitalAPI.ErrorHandling.Exceptions
{
    public class BadRequestException: BaseException
    {
        public BadRequestException(string message) : base(message,(int)HttpStatusCode.BadRequest)
        {
        }
    }
}