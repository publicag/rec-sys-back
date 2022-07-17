using System.Collections.Generic;
using MediatR;
using RecommendationSystem.Application.ViewModels;
using RecommendationSystem.Domain.EntityTypes;

namespace RecommendationSystem.Application.Requests.Movies.Queries.GetPagedMovieDetails
{
    public class GetPagedMovieDetailsQuery : IRequest<IReadOnlyList<MovieDetailsVM>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GenreName Genre { get; set; }
    }
}
