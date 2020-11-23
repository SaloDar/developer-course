using System;
using System.Net;

namespace DeveloperCourse.SecondLesson.Common.Web.Exceptions
{
    public class InternalServerErrorException : ApiException
    {
        public InternalServerErrorException()
            : base(HttpStatusCode.InternalServerError)
        {
        }

        public InternalServerErrorException(string message)
            : base(HttpStatusCode.InternalServerError, message)
        {
        }

        public InternalServerErrorException(string message, Exception ex)
            : base(HttpStatusCode.InternalServerError, message, ex)
        {
        }
    }
}
