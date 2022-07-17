namespace RecommendationSystem.Domain.Entities
{
    public class UserRating : BaseEntity
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public float Rating { get; set; }
    }
}
