using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Application.ViewModels;

namespace RecommendationSystem.Application.Requests.Predictions.Queries.GetUserPrediction
{
    public class GetUserPredictionQueryHandler : IRequestHandler<GetUserPredictionQuery, List<MovieDetailsVM>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRatingRepository _userRatingRepository;
        private readonly IUserPredictionRepository _userPredictionRepository;
        private readonly IMapper _mapper;

        public GetUserPredictionQueryHandler(IMovieRepository movieRepository,
            IUserRatingRepository userRatingRepository, IUserPredictionRepository userPredictionRepository,
            IMapper mapper)
        {
            _movieRepository = movieRepository;
            _userRatingRepository = userRatingRepository;
            _userPredictionRepository = userPredictionRepository;
            _mapper = mapper;
        }

        public async Task<List<MovieDetailsVM>> Handle(GetUserPredictionQuery request, CancellationToken cancellationToken)
        {
            var predictions = await _userPredictionRepository.GetPredictionUsers(request.UserProfiledClass);
            var ratings = await _userRatingRepository.GetMostRated(predictions.Select(p => p.UserId).ToList());
            var recommendedMovies = await _movieRepository
                .GetListOfRecommendedMovies(ratings.Select(r => r.MovieId).ToList());

            var movieVms = new List<MovieDetailsVM>();
            foreach (var recommendedMovie in recommendedMovies)
            {
                movieVms.Add(_mapper.Map<MovieDetailsVM>(recommendedMovie));
            }

            return movieVms;
        }
    }
}
