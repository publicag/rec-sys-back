using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Persistence.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(RecommendationSystemDbContext dbContext) : base(dbContext)
        {

        }

        public Task<List<Movie>> GetListOfRecommendedMovies(List<int> movieIds)
        {
            return _dbContext.Movie.Where(m => movieIds.Contains(m.Id)).ToListAsync();
        }

        public async Task LoadReferencesAsync(Movie entry)
        {
            await _dbContext.Entry(entry).Collection(p => p.Genres).LoadAsync();
        }
    }
}
