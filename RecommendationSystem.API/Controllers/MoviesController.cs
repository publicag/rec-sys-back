using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecommendationSystem.Application.Requests.Movies.Queries.GetMovieDetails;
using RecommendationSystem.Application.Requests.Movies.Queries.GetPagedMovieDetails;
using RecommendationSystem.Application.Requests.Movies.Queries.GetRatedMovies;
using RecommendationSystem.Application.Requests.Predictions.Queries.GetPredictionGroup;
using RecommendationSystem.Application.Requests.Predictions.Queries.GetUserPrediction;
using RecommendationSystem.Domain.EntityTypes;
using RecommendationSystem.Identity.Entities;

namespace RecommendationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public MoviesController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var query = new GetMovieDetailsQuery { Id = id };
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPagedMovies(int page,
            int pageSize, [FromQuery] GenreName genre)
        {
            var query = new GetPagedMovieDetailsQuery
            {
                Page = page,
                PageSize = pageSize,
                Genre = genre
            };
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("prediction")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecommendedMovies()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var predictionQuery = new GetPredictionGroupQuery
            {
                UserId = Guid.Parse(user.Id)
            };
            var predictionResponse = await _mediator.Send(predictionQuery);

            var query = new GetUserPredictionQuery { UserProfiledClass = predictionResponse };
            var response = await _mediator.Send(query);

            return Ok(response);
        }


        [HttpGet("ratedMovies")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRatedMovies()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var query = new GetRatedMoviesQuery
            {
                UserId = Guid.Parse(user.Id)
            };
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
