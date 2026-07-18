using Microsoft.AspNetCore.Mvc;
using ProjectAcademy.EndPointsAndControllers;
using ProjectAcademy.Services;
using ProjectAcademy.Contracts;
using ProjectAcademy.Validation;


namespace ProjectAcademy.AuthControllers
{
    [ApiController]
    [Route("/controller")]
    public class AuthControllers : ControllerBase
    {
        private readonly AuthService _auth;
        private readonly ILogger<AuthControllers> _logger;
        public AuthControllers(AuthService auth, ILogger<AuthControllers> logger)
        {
            _auth = auth; _logger = logger;
        }

        [HttpPost("registration")]

        public async Task<IActionResult> MapRegistration([FromBody] RegisterStudentRequest request)
        {
            try
            {
                await _auth.RegisterStudent(request);
                return Ok();
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
                
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> MapLogin([FromBody] LoginRequest request)
        {
            try
            {
                string jwtToken = await _auth.Login(request);
                return Ok(jwtToken);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, ex.Message);
                return Unauthorized();

            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, ex.Message);
                return Unauthorized();
            }

        }
    }
}
