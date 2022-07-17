using System;

namespace RecommendationSystem.Domain.Entities
{
    public class Rating : BaseEntity
    {
        public Guid UserId { get; set; }
        public Movie Movie { get; set; }
        public float Rate { get; set; }
    }
}
