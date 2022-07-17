using AutoMapper;
using RecommendationSystem.Application.Responses.Ratings;
using RecommendationSystem.Application.ViewModels;
using RecommendationSystem.Domain.Entities;
using RecommendationSystem.Identity.DTO;
using RecommendationSystem.Identity.Entities;

namespace RecommendationSystem.Application.MappingProfiles
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Movie, MovieDetailsVM>().ReverseMap();
            CreateMap<Genre, GenreVM>().ReverseMap();
            CreateMap<RateMovieResponse, Rating>().ReverseMap()
                .ForMember(m => m.MovieId, src => src.MapFrom(p => p.Movie.Id));
            CreateMap<ApplicationUser, UserRegisterCredentials>().ReverseMap();
        }
    }
}
