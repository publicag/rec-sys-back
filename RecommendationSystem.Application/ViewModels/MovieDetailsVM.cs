using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RecommendationSystem.Application.ViewModels
{
    public class MovieDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<GenreVM> Genres { get; set; }
        public int ImdbId { get; set; }
        public int TmdbId { get; set; }
        public string ImageURL { get; set; }
    }
}
