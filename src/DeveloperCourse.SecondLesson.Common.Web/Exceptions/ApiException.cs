using System;
using System.Net;
using DeveloperCourse.SecondLesson.Common.Web.Interfaces;

namespace DeveloperCourse.SecondLesson.Common.Web.Exceptions
{
    public class ApiException : Exception, IHasUserMessage
    {
        public HttpStatusCode StatusCode { get; }

        public string UserMessage { get; }

        public ApiException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message, Exception ex)
            : base(message, ex)
        {
            StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message, string userMessage)
            : base(message)
        {
            StatusCode = statusCode;
            UserMessage = userMessage;
        }

        public ApiException(HttpStatusCode statusCode, string message, Exception ex, string userMessage)
            : base(message, ex)
        {
            StatusCode = statusCode;
            UserMessage = userMessage;
        }
    }
}