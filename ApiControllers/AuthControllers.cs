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
        public AuthControllers(AuthService auth) => _auth = auth;


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
                return Unauthorized();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }

        }
    }
}
