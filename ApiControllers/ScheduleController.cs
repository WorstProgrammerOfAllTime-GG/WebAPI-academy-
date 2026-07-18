using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAcademy.Services;

namespace ProjectAcademy.ApiControllers
{
    [ApiController]
    [Route("/controller")]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _schedule;
        public ScheduleController(ScheduleService schedule) => _schedule = schedule;

        [HttpGet("schedule")]
        [Authorize]
        public async Task<IActionResult> GetSchedule()
        {
            try
            {
                var result = await _schedule.GetSchedule(HttpContext);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
        }
    }
}
