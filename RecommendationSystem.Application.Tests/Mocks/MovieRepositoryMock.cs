using System.Collections.Generic;
using Moq;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Application.Tests.Mocks
{
    public class MovieRepositoryMock
    {
        public static Mock<IMovieRepository> GetMovieRepository()
        {
            var @movie = new Movie() { Id = 1, Title = "Test name" };
            var updatedMovie = new Movie() { Id = 1, Title = "Name test" };
            var movies = new List<Movie>() { @movie };
            var mockMovieRepository = new Mock<IMovieRepository>();

            mockMovieRepository.Setup(rep => rep.AddAsync(It.IsAny<Movie>())).ReturnsAsync((Movie entity) =>
            {
                movies.Add(entity);
                return entity;
            });

            mockMovieRepository.Setup(rep => rep.GetByIdAsync(It.Is<int>(g => g == @movie.Id))).ReturnsAsync(@movie);
            mockMovieRepository.Setup(rep => rep.UpdateAsync(It.Is<Movie>(e => e == @movie))).ReturnsAsync(updatedMovie);
            mockMovieRepository.Setup(rep => rep.ListAllAsync()).ReturnsAsync(movies);
            mockMovieRepository.Setup(rep => rep.DeleteAsync(It.Is<Movie>(e => e == @movie))).Callback((Movie entity) =>
            {
                movies.Remove(entity);
            });

            return mockMovieRepository;
        }
    }
}
