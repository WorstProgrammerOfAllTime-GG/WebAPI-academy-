
using ProjectAcademy.Services;

namespace ProjectAcademy.RestHandlers
{
    
    public static class ScheduleEndPoint
    {
        public static void MapSchedule(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGet("/schedule", async (HttpContext context, DataSchedule schedule) =>
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
