using System.Collections.Generic;

namespace RecommendationSystem.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public int ImdbId { get; set; }
        public int TmdbId { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
