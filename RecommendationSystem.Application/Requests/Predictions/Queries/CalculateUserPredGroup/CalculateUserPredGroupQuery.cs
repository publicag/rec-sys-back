using System;
using System.Collections.Generic;
using MediatR;

namespace RecommendationSystem.Application.Requests.Predictions.Queries.CalculateUserPredGroup
{
    public class CalculateUserPredGroupQuery : IRequest<List<(int, float)>>
    {
        public Guid UserId { get; set; }
    }
}
