using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RecommendationSystem.Application.Exceptions;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Application.Responses.Utils;
using RecommendationSystem.Application.ViewModels;

namespace RecommendationSystem.Application.Requests.Movies.Queries.GetPagedMovieDetails
{
    public class GetPagedMovieDetailsQueryHandler :
        IRequestHandler<GetPagedMovieDetailsQuery, IReadOnlyList<MovieDetailsVM>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly ITmdbMovieConnector _movieConnector;

        public GetPagedMovieDetailsQueryHandler(IMovieRepository movieRepository, IMapper mapper,
            ITmdbMovieConnector tmdbMovieConnector)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _movieConnector = tmdbMovieConnector;
        }

        public async Task<IReadOnlyList<MovieDetailsVM>> Handle(GetPagedMovieDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _movieRepository
                .GetQueryable()
                .Where(m => m.Genres.Any(g => g.Name == request.Genre))
                .OrderBy(m => m.Id);

            var movies = await _movieRepository
                .GetPagedResponseAsync(query, request.Page, request.PageSize);

            if (movies is null || movies.Count == 0)
            {
                throw new NotFoundException(StatusCode.None);
            }

            var listOfMovies = new List<MovieDetailsVM>();

            foreach (var movie in movies)
            {
                var movieVM = _mapper.Map<MovieDetailsVM>(movie);
                movieVM.ImageURL = await _movieConnector.GenerateImagePathAsync(movieVM.TmdbId);
                listOfMovies.Add(movieVM);
            }

            return listOfMovies.AsReadOnly();
        }
    }
}
