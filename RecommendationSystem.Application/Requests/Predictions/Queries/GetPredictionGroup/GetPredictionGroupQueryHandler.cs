using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RecommendationSystem.Application.Exceptions;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Application.Responses.Utils;

namespace RecommendationSystem.Application.Requests.Predictions.Queries.GetPredictionGroup
{
    public class GetPredictionGroupQueryHandler : IRequestHandler<GetPredictionGroupQuery, int>
    {
        private readonly IUserPredictionGroupRepository _userPredictionGroupRepository;
        private readonly HttpClient _client;
        private const string Url = "http://host.docker.internal:5002/api/p/prediction";

        public GetPredictionGroupQueryHandler(IUserPredictionGroupRepository userPredictionGroupRepository,
            HttpClient client)
        {
            _userPredictionGroupRepository = userPredictionGroupRepository;
            _client = client;
        }

        public async Task<int> Handle(GetPredictionGroupQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Incoming request");
            var predictionGroup = await _userPredictionGroupRepository.GetOldestPredGroup(request.UserId);

            if (predictionGroup is null)
            {
                throw new NotFoundException(StatusCode.None, "Prediction group not found");
            }

            Console.WriteLine("Prediction: {0}", predictionGroup.ConvertToArray().ToString());

            var content = JsonSerializer.Serialize(predictionGroup.ConvertToArray());
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Url),
                Content = stringContent
            };

            var predClassResponse = await _client.SendAsync(httpRequest, cancellationToken);
            var predClassContent = await predClassResponse.Content.ReadAsStringAsync(cancellationToken);
            var parsed = int.TryParse(predClassContent[1..^3], out var predClass);

            return parsed
                ? predClass
                : throw new FormatException($"Cannot parse prediction group value: {predClassContent}");
        }
    }
}
