using System;
using System.Linq;
using System.Threading.Tasks;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Persistence.Repositories
{
    public class UserPredictionGroupRepository : BaseRepository<UserPredGroup>, IUserPredictionGroupRepository
    {
        public UserPredictionGroupRepository(RecommendationSystemDbContext dbContext) : base(dbContext)
        {
        }

        public Task<UserPredGroup> GetOldestPredGroup(Guid userId)
        {
            return Task.FromResult(_dbContext.UserPredGroup.Where(g => g.UserId == userId)
                .OrderBy(g => g.CreatedDateTime).LastOrDefault());
        }
    }
}
