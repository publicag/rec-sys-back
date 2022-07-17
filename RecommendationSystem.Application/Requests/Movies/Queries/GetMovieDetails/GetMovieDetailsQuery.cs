using MediatR;
using RecommendationSystem.Application.ViewModels;

namespace RecommendationSystem.Application.Requests.Movies.Queries.GetMovieDetails
{
    public class GetMovieDetailsQuery : IRequest<MovieDetailsVM>
    {
        public int Id { get; set; }
    }
}
