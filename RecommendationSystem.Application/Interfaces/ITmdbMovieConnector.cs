using System.Threading.Tasks;

namespace RecommendationSystem.Application.Interfaces
{
    public interface ITmdbMovieConnector
    {
        public Task<string> GenerateImagePathAsync(int id);
    }
}
