using RecommendationSystem.Application.Responses.Utils;

namespace RecommendationSystem.Application.Responses
{
    public class NotFoundResponse : BaseResponse
    {
        public NotFoundResponse(string message, StatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public StatusCode StatusCode { get; set; }
    }
}
