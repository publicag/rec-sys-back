using System.Text.Json.Serialization;

namespace RecommendationSystem.Application.Responses
{
    public class BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }
        public BaseResponse()
        {

        }

        public BaseResponse(string message)
        {
            Message = message;
        }
    }
}
