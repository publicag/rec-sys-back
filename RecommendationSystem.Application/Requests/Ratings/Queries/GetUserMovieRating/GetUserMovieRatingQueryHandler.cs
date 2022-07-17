using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RecommendationSystem.Application.Interfaces;

namespace RecommendationSystem.Application.Requests.Ratings.Queries
{
    public class GetUserMovieRatingQueryHandler
        : IRequestHandler<GetUserMovieRatingQuery, float>
    {
        private readonly IRatingRepository _ratingRepository;

        public GetUserMovieRatingQueryHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<float> Handle(GetUserMovieRatingQuery request,
            CancellationToken cancellationToken)
        {
            var rating = await _ratingRepository.GetMovieUserRating(request.UserId, request.MovieId);

            return rating is null ? 0 : rating.Rate;
        }
    }
}
