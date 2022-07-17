using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Application.Interfaces
{
    public interface IRatingRepository : IRepository<Rating>
    {
        Task<Rating> GetMovieUserRating(Guid userId, int movieId);
        Task<List<Rating>> GetUserRatingList(Guid userId);
        Task<List<Rating>> GetUserRatingListIncludedMoviesAsync(Guid userId);
    }
}
