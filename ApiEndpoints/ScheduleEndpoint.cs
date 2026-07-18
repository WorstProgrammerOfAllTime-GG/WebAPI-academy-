
using Microsoft.AspNetCore.Mvc;
using ProjectAcademy.Services;

namespace ProjectAcademy.RestHandlers
{
    
    public static class ScheduleEndpoint
    {
        public static void MapSchedule(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGet("endpoint/schedule", async (HttpContext context, ScheduleService schedule) =>
            {
                try
                {
                    var mySchedule = await schedule.GetSchedule(context);
                    return Results.Ok(mySchedule);
                } catch (UnauthorizedAccessException ex)
                {
                    return Results.Unauthorized();
                }
            }).RequireAuthorization();
        }
    }
}
