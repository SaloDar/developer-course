using System;
using System.Net;

namespace DeveloperCourse.SecondLesson.Common.Web.Exceptions
{
    public class TooManyRequestsException : ApiException
    {
        public TooManyRequestsException()
            : base(HttpStatusCode.TooManyRequests)
        {
        }

        public TooManyRequestsException(string message)
            : base(HttpStatusCode.TooManyRequests, message)
        {
        }

        public TooManyRequestsException(string message, Exception ex)
            : base(HttpStatusCode.TooManyRequests, message, ex)
        {
        }

        public TooManyRequestsException(string message, string userMessage)
            : base(HttpStatusCode.TooManyRequests, message, userMessage)
        {
        }

        public TooManyRequestsException(string message, Exception ex, string userMessage)
            : base(HttpStatusCode.TooManyRequests, message, ex, userMessage)
        {
        }
    }
}
