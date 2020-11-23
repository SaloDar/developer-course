using System;
using System.Net;

namespace DeveloperCourse.SecondLesson.Common.Web.Exceptions
{
    public class ServiceUnavailableException : ApiException
    {
        public ServiceUnavailableException()
            : base(HttpStatusCode.ServiceUnavailable)
        {
        }

        public ServiceUnavailableException(string message)
            : base(HttpStatusCode.ServiceUnavailable, message)
        {
        }

        public ServiceUnavailableException(string message, Exception ex)
            : base(HttpStatusCode.ServiceUnavailable, message, ex)
        {
        }

        public ServiceUnavailableException(string message, string userMessage)
            : base(HttpStatusCode.ServiceUnavailable, message, userMessage)
        {
        }

        public ServiceUnavailableException(string message, Exception ex, string userMessage)
            : base(HttpStatusCode.ServiceUnavailable, message, ex, userMessage)
        {
        }
    }
}