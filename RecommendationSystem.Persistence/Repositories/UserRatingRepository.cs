using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Persistence.Repositories
{
    public class UserRatingRepository : BaseRepository<UserRating>, IUserRatingRepository
    {
        public UserRatingRepository(RecommendationSystemDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<UserRating>> GetMostRated(List<int> userIds)
        {
            return _dbContext.UserRating.Where(r => userIds.Contains(r.UserId))
                .OrderByDescending(r => r.Rating).Take(15).ToListAsync();
        }
    }
}
