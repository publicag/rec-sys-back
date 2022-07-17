using System;
using MediatR;

namespace RecommendationSystem.Application.Requests.Predictions.Queries.GetPredictionGroup
{
    public class GetPredictionGroupQuery : IRequest<int>
    {
        public Guid UserId { get; set; }
    }
}
