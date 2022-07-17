using FluentValidation;

namespace RecommendationSystem.Application.Requests.Ratings.Commands.UpdateRating
{
    public class UpdateMovieRateCommandValidator : AbstractValidator<UpdateMovieRateCommand>
    {
        public UpdateMovieRateCommandValidator()
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
