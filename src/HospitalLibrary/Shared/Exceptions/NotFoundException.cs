using System.Net;

namespace HospitalLibrary.Shared.Exceptions
{
    public class FoundException: BaseException
    {
        public FoundException(string message) : base(message, (int)HttpStatusCode.NotFound)
        {
        }
    }
}