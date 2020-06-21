using System;
using System.Net;

namespace AnimeTracker
{
    public class JikanRequestException : Exception
    {
        public HttpStatusCode ResponseCode { get; private set; }

        public JikanRequestException()
        {
            
        }

        public JikanRequestException(string message) : base(message)
        {

        }

        public JikanRequestException(string message, HttpStatusCode responseCode) : base(message)
        {
            ResponseCode = responseCode;
        }

        public JikanRequestException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}