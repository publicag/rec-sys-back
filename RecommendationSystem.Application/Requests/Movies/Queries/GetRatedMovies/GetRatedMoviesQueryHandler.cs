using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Application.ViewModels;

namespace RecommendationSystem.Application.Requests.Movies.Queries.GetRatedMovies
{
    public class GetRatedMoviesQueryHandler : IRequestHandler<GetRatedMoviesQuery, IReadOnlyList<MovieDetailsVM>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IRatingRepository _ratingRepository;

        public GetRatedMoviesQueryHandler(IMovieRepository movieRepository, IMapper mapper, IRatingRepository ratingRepository)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _ratingRepository = ratingRepository;
        }

        public async Task<IReadOnlyList<MovieDetailsVM>> Handle(GetRatedMoviesQuery request, CancellationToken cancellationToken)
        {
            var userRatings = await _ratingRepository.GetUserRatingListIncludedMoviesAsync(request.UserId);
            var userRatedMovies = await _movieRepository
                .GetListOfRecommendedMovies(userRatings
                    .Select(rating => rating.Movie.Id)
                    .ToList());

            var movieVms = new List<MovieDetailsVM>();
            foreach (var ratedMovie in userRatedMovies)
            {
                movieVms.Add(_mapper.Map<MovieDetailsVM>(ratedMovie));
            }

            return movieVms.AsReadOnly();
        }
    }
}
