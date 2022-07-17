using System.Collections.Generic;
using System.Threading.Tasks;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Application.Interfaces
{
    public interface IUserRatingRepository : IRepository<UserRating>
    {
        Task<List<UserRating>> GetMostRated(List<int> userIds);
    }
}
