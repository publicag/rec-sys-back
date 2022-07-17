using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RecommendationSystem.Application.Exceptions;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Application.Responses.Ratings;
using RecommendationSystem.Application.Responses.Utils;

namespace RecommendationSystem.Application.Requests.Ratings.Commands.UpdateRating
{
    public class UpdateMovieRateCommandHandler : IRequestHandler<UpdateMovieRateCommand, RateMovieResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRatingRepository _ratingRepository;

        public UpdateMovieRateCommandHandler(IMapper mapper, IRatingRepository ratingRepository)
        {
            _mapper = mapper;
            _ratingRepository = ratingRepository;
        }

        public async Task<RateMovieResponse> Handle(UpdateMovieRateCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateMovieRateCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var existingRate = await _ratingRepository.GetMovieUserRating(request.UserId, request.MovieId);

            if (existingRate is null)
            {
                throw new NotFoundException(StatusCode.None, "Rate for movie not found");
            }

            existingRate.Rate = request.Rate;
            var updated = await _ratingRepository.UpdateAsync(existingRate);

            return _mapper.Map<RateMovieResponse>(updated);
        }
    }
}
