using System;
using MediatR;
using RecommendationSystem.Application.Responses.Ratings;

namespace RecommendationSystem.Application.Requests.Ratings.Commands.UpdateRating
{
    public class UpdateMovieRateCommand : IRequest<RateMovieResponse>
    {
        public Guid UserId { get; set; }
        public int MovieId { get; set; }
        public float Rate { get; set; }
    }
}
