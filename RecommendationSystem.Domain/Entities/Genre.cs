using RecommendationSystem.Domain.EntityTypes;

namespace RecommendationSystem.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public GenreName Name { get; set; }
    }
}
