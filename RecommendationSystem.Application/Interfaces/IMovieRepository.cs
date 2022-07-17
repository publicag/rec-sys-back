using System.Collections.Generic;
using System.Threading.Tasks;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Application.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task LoadReferencesAsync(Movie entry);
        Task<List<Movie>> GetListOfRecommendedMovies(List<int> movieIds);
    }
}
