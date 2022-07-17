using System;
using RecommendationSystem.Application.Responses.Utils;

namespace RecommendationSystem.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(StatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(StatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public NotFoundException(StatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public StatusCode StatusCode { get; set; }
    }
}
