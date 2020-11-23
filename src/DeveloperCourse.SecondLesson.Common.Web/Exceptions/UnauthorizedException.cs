using System;
using System.Net;

namespace DeveloperCourse.SecondLesson.Common.Web.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException()
            : base(HttpStatusCode.Unauthorized)
        {
        }

        public UnauthorizedException(string message)
            : base(HttpStatusCode.Unauthorized, message)
        {
        }

        public UnauthorizedException(string message, Exception ex)
            : base(HttpStatusCode.Unauthorized, message, ex)
        {
        }

        public UnauthorizedException(string message, string userMessage)
            : base(HttpStatusCode.Unauthorized, message, userMessage)
        {
        }

        public UnauthorizedException(string message, Exception ex, string userMessage)
            : base(HttpStatusCode.Unauthorized, message, ex, userMessage)
        {
        }
    }
}
