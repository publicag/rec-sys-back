using System;
using MediatR;

namespace RecommendationSystem.Application.Requests.Ratings.Queries
{
    public class GetUserMovieRatingQuery : IRequest<float>
    {
        public Guid UserId { get; set; }
        public int MovieId { get; set; }
    }
}
