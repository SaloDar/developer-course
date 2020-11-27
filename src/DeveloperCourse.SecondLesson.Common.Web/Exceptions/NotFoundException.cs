using System;
using System.Net;

namespace DeveloperCourse.SecondLesson.Common.Web.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException()
            : base(HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(HttpStatusCode.NotFound, message)
        {
        }

        public NotFoundException(string message, Exception ex)
            : base(HttpStatusCode.NotFound, message, ex)
        {
        }

        public NotFoundException(string message, string userMessage)
            : base(HttpStatusCode.NotFound, message, userMessage)
        {
        }

        public NotFoundException(string message, Exception ex, string userMessage)
            : base(HttpStatusCode.NotFound, message, ex, userMessage)
        {
        }
    }
}
