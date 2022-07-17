using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using RecommendationSystem.Application.Exceptions;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Application.MappingProfiles;
using RecommendationSystem.Application.Requests.Movies.Queries.GetMovieDetails;
using RecommendationSystem.Application.Tests.Mocks;
using Shouldly;
using Xunit;

namespace RecommendationSystem.Application.Tests.Movies.Queries
{
    public class GetMovieDetailsQueryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMovieRepository> _mockEventRepository;

        public GetMovieDetailsQueryTests()
        {
            _mockEventRepository = MovieRepositoryMock.GetMovieRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationMappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ExistingId_ReturnsFromEventRepository()
        {
            var handler = new GetMovieDetailsQueryHandler(_mockEventRepository.Object, _mapper);
            var request = new GetMovieDetailsQuery() { Id = 1 };
            var response = await handler.Handle(request, CancellationToken.None);

            response.ShouldNotBeNull();
            response.Title.ShouldBe("Test name");
        }

        [Fact]
        public async Task Handle_NonExistingId_ThrowsNotFoundError()
        {
            var handler = new GetMovieDetailsQueryHandler(_mockEventRepository.Object, _mapper);
            var request = new GetMovieDetailsQuery() { Id = 2 };

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}
