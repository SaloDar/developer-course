using System;
using System.Net;

namespace DeveloperCourse.SecondLesson.Common.Web.Exceptions
{
    public class UnsupportedMediaTypeException : ApiException
    {
        public UnsupportedMediaTypeException()
            : base(HttpStatusCode.UnsupportedMediaType)
        {
        }

        public UnsupportedMediaTypeException(string message)
            : base(HttpStatusCode.UnsupportedMediaType, message)
        {
        }

        public UnsupportedMediaTypeException(string message, Exception ex)
            : base(HttpStatusCode.UnsupportedMediaType, message, ex)
        {
        }

        public UnsupportedMediaTypeException(string message, string userMessage)
            : base(HttpStatusCode.UnsupportedMediaType, message, userMessage)
        {
        }

        public UnsupportedMediaTypeException(string message, Exception ex, string userMessage)
            : base(HttpStatusCode.UnsupportedMediaType, message, ex, userMessage)
        {
        }
    }
}