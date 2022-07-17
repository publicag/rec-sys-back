using System;
using System.Threading.Tasks;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Application.Interfaces
{
    public interface IUserPredictionGroupRepository : IRepository<UserPredGroup>
    {
        Task<UserPredGroup> GetOldestPredGroup(Guid userId);
    }
}
