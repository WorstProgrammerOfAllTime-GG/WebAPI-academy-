using Microsoft.AspNetCore.Mvc;
using ProjectAcademy.Services;
using ProjectAcademy.Validation;
using ProjectAcademy.Contracts;
namespace ProjectAcademy.EndPointsAndControllers
{
    public static class AuthEndpoints
    {     
        public static void MapAuthEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapPost("endpoint/registration", async (RegisterStudentRequest request, AuthService reg)=>
            {
                try
                {
                    await reg.RegisterStudent(request);
                    return Results.Ok();
                } catch (ValidationException ex)
                {               
                    return Results.BadRequest(new
                    {
                        message = ex.Message,
                    });                      
                }
                          
            });
            routeBuilder.MapPost("endpoint/login", async (LoginRequest request, AuthService login)=>
            {
                try
                {
                   string jwtToken = await login.Login(request);
                   return Results.Ok(jwtToken);
                }  
                catch (NullReferenceException ex)
                {
                    return Results.Unauthorized();                               
                } catch (UnauthorizedAccessException ex)
                {
                    return Results.Unauthorized();
                }
                
            });
        }   
    }
}
