using System.Net;

namespace HospitalLibrary.Shared.Exceptions
{
    public class BadRequestException: BaseException
    {
        public BadRequestException(string message) : base(message,(int)HttpStatusCode.BadRequest)
        {
        }
    }
}