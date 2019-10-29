using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FizzBuzz
{
    [ApiController]
    [Route("[controller]")]
    public class FizzBuzzController : ControllerBase
    {
        private readonly IFizzBuzzService _service;
        private readonly ILogger _logger;

        public FizzBuzzController(IFizzBuzzService service, ILogger<FizzBuzzController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        [ProducesResponseType(typeof(FizzBuzzResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFizzBuzz()
        {
            try
            {
                var results = await _service.FizzBuzz();

                var response = new FizzBuzzResponse {Results = results};

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is ArgumentOutOfRangeException || e is ArgumentNullException)
                    return BadRequest(e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ResponseCache(Duration = 30)]
        [ProducesResponseType(typeof(FizzBuzzResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostFizzBuzz(FizzBuzzRequest request)
        {
            try
            {
                var results = await _service.FizzBuzz(request.MinValue, request.MaxValue, request.Multiples.ToArray());
        
                var response = new FizzBuzzResponse { Results = results };
        
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
        
                if (e is ArgumentOutOfRangeException || e is ArgumentNullException)
                    return BadRequest(e.Message);
        
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
