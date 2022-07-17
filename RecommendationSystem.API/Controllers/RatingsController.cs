using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RecommendationSystem.Application.Requests.Predictions.Queries.CalculateUserPredGroup;
using RecommendationSystem.Application.Requests.Ratings.Commands.CreateRating;
using RecommendationSystem.Application.Requests.Ratings.Commands.UpdateRating;
using RecommendationSystem.Application.Requests.Ratings.Queries;
using RecommendationSystem.Application.ViewModels;
using RecommendationSystem.Identity.Entities;

namespace RecommendationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public RatingsController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRatingByMovieUser(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user is not null)
            {
                var query = new GetUserMovieRatingQuery
                {
                    MovieId = id,
                    UserId = Guid.Parse(user.Id)
                };
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("rateMovie")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RateMovie(RateMovieVM request)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var command = new RateMovieCommand
            {
                UserId = Guid.Parse(user.Id),
                MovieId = request.MovieId,
                Rate = request.Rate
            };
            var response = await _mediator.Send(command);
            await GeneratePredictionGroups();

            return Ok(response);
        }

        [HttpPut("updateMovieRate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMovieRate(RateMovieVM request)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var command = new UpdateMovieRateCommand
            {
                UserId = Guid.Parse(user.Id),
                MovieId = request.MovieId,
                Rate = request.Rate
            };

            var response = await _mediator.Send(command);
            await GeneratePredictionGroups();

            return Ok(response);
        }


        [HttpGet("generatePredGroups")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<float>> GeneratePredictionGroups()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var query = new CalculateUserPredGroupQuery
            {
                UserId = Guid.Parse(user.Id)
            };

            var response = await _mediator.Send(query);

            return response.SelectMany(t => new[] { t.Item1, t.Item2 }).Distinct();
        }
    }
}
