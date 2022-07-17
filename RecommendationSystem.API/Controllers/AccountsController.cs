using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecommendationSystem.Application.Requests.Predictions.Queries.GetPredictionGroup;
using RecommendationSystem.Identity.DTO;
using RecommendationSystem.Identity.Entities;
using RecommendationSystem.Identity.JWT;

namespace RecommendationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtManager _jwtManager;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public AccountsController(UserManager<ApplicationUser> userManager, JwtManager jwtManager, IMapper mapper,
            IMediator mediator)
        {
            _userManager = userManager;
            _jwtManager = jwtManager;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(UserAuthCredentials credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, credentials.Password))
            {
                return Unauthorized(new UserAuthResponse { Error = "Invalid credentials." });
            }

            var claims = JwtManager.GetClaims(user);
            var signingCredentials = _jwtManager.GetSigningCredentials();
            var tokenOptions = _jwtManager.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            Console.WriteLine("Generating prediction");
            var query = new GetPredictionGroupQuery
            {
                UserId = Guid.Parse(user.Id)
            };

            var response = await _mediator.Send(query);
            Console.WriteLine(response);

            return Ok(new UserAuthResponse { Token = token, ExpiresIn = _jwtManager.ExpirationTime });
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(UserRegisterCredentials credentials)
        {
            if (await _userManager.FindByEmailAsync(credentials.Email) is not null)
            {
                return BadRequest(new UserAuthResponse { Error = "Email already exists." });
            }

            var user = _mapper.Map<ApplicationUser>(credentials);
            try
            {
                await _userManager.CreateAsync(user, credentials.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new UserAuthResponse { Error = e.Message });
            }
        }

        [HttpGet("generatePred")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GeneratePredictionClass()
        {
            Console.WriteLine("Prediction class generation");
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var query = new GetPredictionGroupQuery
            {
                UserId = Guid.Parse(user.Id)
            };

            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
