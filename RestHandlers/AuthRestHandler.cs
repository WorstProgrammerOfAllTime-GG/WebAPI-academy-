using Microsoft.AspNetCore.Mvc;
using ProjectAcademy.Services;
using ProjectAcademy.Validation;

namespace ProjectAcademy.EndPointsAndControllers
{
    public static class AuthEndPoint
    {
        public static void MapAuth(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapPost("/registration", async (RequestReg request, Authentication reg)=>
            {
                try
                {
                    await reg.Registration(request);
                    return Results.Ok();
                } catch (ValidationException ex)
                {               
                    return Results.BadRequest(new
                    {
                        message = ex.Message,
                    });                      
                }
                          
            });
            routeBuilder.MapPost("/login", async (RequestLogin request, Authentication login)=>
            {
                try
                {
                    await login.Login(request);
                    return Results.Ok();
                } catch (ArgumentNullException ex)
                {
                    return Results.BadRequest(new
                    {
                        message = ex.Message
                    });

                } catch (AuthException ex)
                {
                    return Results.BadRequest(new
                    {
                        message = ex.Message
                    });
                }
                
            });
        }   
    }

    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly Authentication _auth;
        public AuthController(Authentication auth)=> _auth = auth;
        
        [HttpPost("reg")]
        
        public async Task<IActionResult> MapRegistration([FromBody] RequestReg request)
        {
            try
            {
                await _auth.Registration(request);
                return Ok();
            } catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            
        }
        [HttpPost("login")]
        public async Task<IActionResult> MapLogin([FromBody] RequestLogin request)
        {
            try
            {
                await _auth.Login(request);
                return Ok();
            } catch (ArgumentNullException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            } catch (AuthException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            
        }
    }

    public record RequestReg(
    string FullName,
    string Email,
    string PhoneNumber,
    string Password
);

    
    public record RequestLogin(
        string Password,
        string? Email = "",
        string? PhoneNumber = ""
    );

}
