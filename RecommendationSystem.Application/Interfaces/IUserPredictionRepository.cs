using System.Collections.Generic;
using System.Threading.Tasks;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Application.Interfaces
{
    public interface IUserPredictionRepository : IRepository<UserPrediction>
    {
        Task<List<UserPrediction>> GetPredictionUsers(int predictionGroup);
    }
}
