namespace RecommendationSystem.Domain.Entities
{
    public class UserPrediction : BaseEntity
    {
        public int UserId { get; set; }
        public int PredClass { get; set; }
    }
}
