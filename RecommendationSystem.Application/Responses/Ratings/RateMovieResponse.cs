using System;

namespace RecommendationSystem.Application.Responses.Ratings
{
    public class RateMovieResponse : BaseResponse
    {
        public Guid UserId { get; set; }
        public int MovieId { get; set; }
        public float Rate { get; set; }
    }
}
