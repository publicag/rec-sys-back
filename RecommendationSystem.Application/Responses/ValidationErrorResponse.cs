using System.Collections.Generic;

namespace RecommendationSystem.Application.Responses
{
    public class ValidationErrorResponse : BaseResponse
    {
        public ValidationErrorResponse(List<string> validationErrors, string message) : base(message)
        {
            ValidationErrors = validationErrors;
        }

        public List<string> ValidationErrors { get; set; }
    }
}
