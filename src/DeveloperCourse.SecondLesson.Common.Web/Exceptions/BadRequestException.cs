using System;
using System.Net;

namespace DeveloperCourse.SecondLesson.Common.Web.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException()
            : base(HttpStatusCode.BadRequest)
        {
        }

        public BadRequestException(string message)
            : base(HttpStatusCode.BadRequest, message)
        {
        }

        public BadRequestException(string message, Exception ex)
            : base(HttpStatusCode.BadRequest, message, ex)
        {
        }

        public BadRequestException(string message, string userMessage)
            : base(HttpStatusCode.BadRequest, message, userMessage)
        {
        }

        public BadRequestException(string message, Exception ex, string userMessage)
            : base(HttpStatusCode.BadRequest, message, ex, userMessage)
        {
        }
    }
}
