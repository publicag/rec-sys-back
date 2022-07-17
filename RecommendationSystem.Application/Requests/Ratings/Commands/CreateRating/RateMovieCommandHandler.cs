using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RecommendationSystem.Application.Exceptions;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Application.Responses.Ratings;
using RecommendationSystem.Application.Responses.Utils;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Application.Requests.Ratings.Commands.CreateRating
{
    public class RateMovieCommandHandler : IRequestHandler<RateMovieCommand, RateMovieResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRatingRepository _ratingRepository;
        private readonly IMovieRepository _movieRepository;

        public RateMovieCommandHandler(IMapper mapper, IRatingRepository ratingRepository, IMovieRepository movieRepository)
        {
            _mapper = mapper;
            _ratingRepository = ratingRepository;
            _movieRepository = movieRepository;
        }

        public async Task<RateMovieResponse> Handle(RateMovieCommand request, CancellationToken cancellationToken)
        {
            var validator = new RateMovieCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var movie = await _movieRepository.GetByIdAsync(request.MovieId);

            if (movie is null)
            {
                throw new NotFoundException(StatusCode.None, "Movie for rate not found.");
            }

            var rate = new Rating
            {
                Movie = movie,
                UserId = request.UserId,
                Rate = request.Rate
            };

            var rating = await _ratingRepository.AddAsync(rate);

            return _mapper.Map<RateMovieResponse>(rating);
        }
    }
}
