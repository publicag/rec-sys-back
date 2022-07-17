using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecommendationSystem.ML.Predictor;

namespace ReccomendationSystem.PredictionService.Controllers
{
    [Route("api/p/[controller]")]
    [ApiController]
    public class PredictionController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPrediciton(List<double> args)
        {
            try
            {
                var predictionResult = Predictor.Predict(args);
                return Ok(predictionResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
