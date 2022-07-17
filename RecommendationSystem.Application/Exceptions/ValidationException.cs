using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace RecommendationSystem.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(ValidationResult validationResult, string message = "") : base(message)
        {
            ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        }

        public List<string> ValidationErrors { get; set; }
    }
}
