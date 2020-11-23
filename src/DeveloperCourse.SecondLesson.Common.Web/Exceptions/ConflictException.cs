using System;
using System.Net;

namespace DeveloperCourse.SecondLesson.Common.Web.Exceptions
{
    public class ConflictException : ApiException
    {
        public ConflictException()
            : base(HttpStatusCode.Conflict)
        {
        }

        public ConflictException(string message)
            : base(HttpStatusCode.Conflict, message)
        {
        }

        public ConflictException(string message, Exception ex)
            : base(HttpStatusCode.Conflict, message, ex)
        {
        }

        public ConflictException(string message, string userMessage)
            : base(HttpStatusCode.Conflict, message, userMessage)
        {
        }

        public ConflictException(string message, Exception ex, string userMessage)
            : base(HttpStatusCode.Conflict, message, ex, userMessage)
        {
        }
    }
}