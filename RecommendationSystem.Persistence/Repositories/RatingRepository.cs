using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Persistence.Repositories
{
    public class RatingRepository : BaseRepository<Rating>, IRatingRepository
    {
        public RatingRepository(RecommendationSystemDbContext dbContext) : base(dbContext)
        {

        }

        public Task<List<Rating>> GetUserRatingList(Guid userId)
        {
            return _dbContext.Rating.Where(r => r.UserId == userId)
                .Include(r => r.Movie).ToListAsync();
        }

        public Task<Rating> GetMovieUserRating(Guid userId, int movieId)
        {
            return Task.Factory.StartNew(() => _dbContext.Rating
                .Where(p => p.UserId == userId && p.Movie.Id == movieId)
                .FirstOrDefault());
        }

        public Task<List<Rating>> GetUserRatingListIncludedMoviesAsync(Guid userId)
        {
            return _dbContext.Rating.Where(r => r.UserId == userId)
                .Include(r => r.Movie).ToListAsync();
        }
    }
}
