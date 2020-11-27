using System;
using System.Net;

namespace DeveloperCourse.SecondLesson.Common.Web.Exceptions
{
    public class ForbiddenException : ApiException
    {
        public ForbiddenException()
            : base(HttpStatusCode.Forbidden)
        {
        }

        public ForbiddenException(string message)
            : base(HttpStatusCode.Forbidden, message)
        {
        }

        public ForbiddenException(string message, Exception ex)
            : base(HttpStatusCode.Forbidden, message, ex)
        {
        }

        public ForbiddenException(string message, string userMessage)
            : base(HttpStatusCode.Forbidden, message, userMessage)
        {
        }

        public ForbiddenException(string message, Exception ex, string userMessage)
            : base(HttpStatusCode.Forbidden, message, ex, userMessage)
        {
        }
    }
}