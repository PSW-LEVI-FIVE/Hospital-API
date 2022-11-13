using System.Net;

namespace HospitalLibrary.Shared.Exceptions
{
    public class UnauthorizedException: BaseException
    {
        public UnauthorizedException(string message) : base(message, (int)HttpStatusCode.Unauthorized)
        {
        }
    }
}