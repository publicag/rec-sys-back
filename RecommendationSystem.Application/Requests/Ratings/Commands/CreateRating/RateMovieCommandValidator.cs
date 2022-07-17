using FluentValidation;

namespace RecommendationSystem.Application.Requests.Ratings.Commands.CreateRating
{
    public class RateMovieCommandValidator : AbstractValidator<RateMovieCommand>
    {
        public RateMovieCommandValidator()
        {
            RuleFor(p => p.MovieId)
                .NotEmpty()
                .NotNull();
            RuleFor(p => p.UserId)
                .NotNull();
            RuleFor(p => p.Rate)
                .InclusiveBetween(0.5f, 5.0f)
                .NotEmpty();
        }
    }
}
