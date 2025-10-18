using EventsManagement.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsManagement.Controllers.Employee
{
    [Authorize(Roles ="Employee")]
    [Route("api/employee/statistics")]
    [ApiController]
    public class EmployeeStatisticsController(IStatisticsService _StatisticsService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _StatisticsService.GetAsync();

            if(data.Status==200)
                return Ok(data);

            return StatusCode(500,data);

        }



    }
}
