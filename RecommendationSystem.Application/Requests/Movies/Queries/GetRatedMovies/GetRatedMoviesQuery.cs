using System;
using System.Collections.Generic;
using MediatR;
using RecommendationSystem.Application.ViewModels;

namespace RecommendationSystem.Application.Requests.Movies.Queries.GetRatedMovies
{
    public class GetRatedMoviesQuery : IRequest<IReadOnlyList<MovieDetailsVM>>
    {
        public Guid UserId { get; set; }
    }
}
