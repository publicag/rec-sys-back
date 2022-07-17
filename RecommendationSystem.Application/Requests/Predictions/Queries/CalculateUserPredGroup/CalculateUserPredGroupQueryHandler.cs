using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Application.Requests.Predictions.Queries.CalculateUserPredGroup
{
    public class CalculateUserPredGroupQueryHandler : IRequestHandler<CalculateUserPredGroupQuery, List<(int, float)>>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserPredictionGroupRepository _userPredictionGroupRepository;
        private readonly IMovieRepository _movieRepository;

        public CalculateUserPredGroupQueryHandler(IRatingRepository ratingRepository,
            IUserPredictionGroupRepository userPredictionGroupRepository,
            IMovieRepository movieRepository)
        {
            _ratingRepository = ratingRepository;
            _userPredictionGroupRepository = userPredictionGroupRepository;
            _movieRepository = movieRepository;
        }

        public async Task<List<(int, float)>> Handle(CalculateUserPredGroupQuery request, CancellationToken cancellationToken)
        {
            var userRatings = await _ratingRepository.GetUserRatingList(request.UserId);
            var ratingGroups = await CalculateUserPredGroup(userRatings);
            var userPredGroup = new UserPredGroup
            {
                UserId = request.UserId,
                FirstGroup = ratingGroups[0].Item1,
                FirstGroupScore = ratingGroups[0].Item2,
                SecondGroup = ratingGroups[1].Item1,
                SecondGroupScore = ratingGroups[1].Item2,
                ThirdGroup = ratingGroups[2].Item1,
                ThirdGroupScore = ratingGroups[2].Item2,
                FourthGroup = ratingGroups[3].Item1,
                FourthGroupScore = ratingGroups[3].Item2,
                FifthGroup = ratingGroups[4].Item1,
                FifthGroupScore = ratingGroups[4].Item2
            };
            await _userPredictionGroupRepository.AddAsync(userPredGroup);

            return await Task.FromResult(ratingGroups);
        }

        private async Task<List<(int, float)>> CalculateUserPredGroup(List<Rating> ratings)
        {
            var calculatedUserPredList = new List<(int, float)>();
            var listOfRatings = new List<(int, float)>();
            var genres = new HashSet<int>();

            foreach (var rating in ratings)
            {
                await _movieRepository.LoadReferencesAsync(rating.Movie);
                var movieGenre = rating.Movie.Genres.FirstOrDefault();
                if (movieGenre != null)
                {
                    var movieGenreValue = (int)movieGenre.Name;
                    var ratingGenre = (movieGenreValue, rating.Rate);
                    if (!genres.Contains(movieGenreValue))
                    {
                        genres.Add(movieGenreValue);
                    }
                    listOfRatings.Add(ratingGenre);
                }
            }

            foreach (var genre in genres)
            {
                var listOfGenreRatings = listOfRatings.Where(r => r.Item1 == genre);
                var ratingLen = listOfGenreRatings.Count();
                var ratingSum = listOfGenreRatings.Sum(r => r.Item2);
                calculatedUserPredList.Add((genre, ratingSum / ratingLen));
            }

            var orderedUserPred = calculatedUserPredList.OrderBy(r => r.Item1).ToList();

            if (orderedUserPred.Count > 5)
            {
                orderedUserPred.RemoveRange(5, orderedUserPred.Count - 5);
            }
            else
            {
                while (orderedUserPred.Count < 5)
                {
                    var rand = new Random();
                    orderedUserPred.Add((rand.Next(1, 15), (float)(rand.NextDouble() * ((5.0 - 0.5) + 0.5))));
                }
            }

            return orderedUserPred;
        }
    }
}
