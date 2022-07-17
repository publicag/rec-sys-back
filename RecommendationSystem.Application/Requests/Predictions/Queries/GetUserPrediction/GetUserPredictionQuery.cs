using System.Collections.Generic;
using MediatR;
using RecommendationSystem.Application.ViewModels;

namespace RecommendationSystem.Application.Requests.Predictions.Queries.GetUserPrediction
{
    public class GetUserPredictionQuery : IRequest<List<MovieDetailsVM>>
    {
        public int UserProfiledClass { get; set; }
    }
}
