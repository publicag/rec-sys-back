using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RecommendationSystem.Application.Exceptions;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Application.Responses.Utils;
using RecommendationSystem.Application.ViewModels;

namespace RecommendationSystem.Application.Requests.Movies.Queries.GetMovieDetails
{
    public class GetMovieDetailsQueryHandler : IRequestHandler<GetMovieDetailsQuery, MovieDetailsVM>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public GetMovieDetailsQueryHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<MovieDetailsVM> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(request.Id);
            await _movieRepository.LoadReferencesAsync(movie);

            return movie is null
                ? throw new NotFoundException(StatusCode.None)
                : _mapper.Map<MovieDetailsVM>(movie);
        }
    }
}
