using System.Net;

namespace HospitalAPI.ErrorHandling.Exceptions
{
    public class NotFoundException: BaseException
    {
        public NotFoundException(string message) : base(message, (int)HttpStatusCode.NotFound)
        {
        }
    }
}