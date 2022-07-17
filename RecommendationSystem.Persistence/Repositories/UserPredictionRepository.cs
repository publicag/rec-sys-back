using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Persistence.Repositories
{
    public class UserPredictionRepository : BaseRepository<UserPrediction>, IUserPredictionRepository
    {
        public UserPredictionRepository(RecommendationSystemDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<UserPrediction>> GetPredictionUsers(int predictionGroup)
        {
            return _dbContext.UserPred.Where(x => x.PredClass == predictionGroup).Take(5).ToListAsync();
        }
    }
}
