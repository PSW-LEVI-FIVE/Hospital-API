using System;

namespace HospitalAPI.ErrorHandling.Exceptions
{
    public class BaseException: Exception
    {
        public int StatusCode;

        public BaseException(string message, int statusCode): base(message)
        {
            StatusCode = statusCode;
        }
        
    }
}