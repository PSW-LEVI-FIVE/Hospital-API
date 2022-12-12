using System.Net;

namespace HospitalLibrary.Shared.Exceptions
{
    public class NotFoundException: BaseException
    {
        public NotFoundException(string message) : base(message, (int)HttpStatusCode.NotFound)
        {
        }
    }
}