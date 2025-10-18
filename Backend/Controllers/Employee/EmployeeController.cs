
using EventsManagement.Interfaces.Services.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventsManagement.Controllers.Employee
{
  
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController(IEmployeeService _EmployeeService) : ControllerBase
    {


        [HttpPost("login/")]
        public async Task<IActionResult> LoginEmployeeAsync(Dtos.Employee.LoginEmployeeDto form)
        {
           
            var data = await _EmployeeService.LoginAsync(form);

            if (data.Status == 200)
                return Ok(data);

            else if (data.Status == 401)
                return BadRequest(data);

                return StatusCode(500, data);

        }

        [Authorize(Roles = "Employee")]
        [HttpGet("by-id/")]
        public async Task<IActionResult> GetAsync()
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var data = await _EmployeeService.GetInfoByIdAsync(int.Parse(Id));

            if (data.Status == 200)
                return Ok(data);

            return StatusCode(500, data);

        }

        [Authorize(Roles = "Employee")]
        [HttpGet("clubs/requests/")]
        public async Task<IActionResult> GetRequestsAsync()
        {
            var data = await _EmployeeService.GetClubsRequestsAsync();

            if (data.Status == 200)
                return Ok(data);

            return StatusCode(500, data);

        }

        [HttpPost("clubs/requests/{RequestId}")]
        public async Task<IActionResult> AcceptCreationRequestAsync(int RequestId)
        {
            var data = await _EmployeeService.AcceptClubCreationRequestAsync(RequestId);

            if (data.Status == 200)
                return Ok(data);

            return StatusCode(500, data);

        }

        [HttpDelete("clubs/requests/{RequestId}")]
        public async Task<IActionResult> DeleteCreationRequestAsync(int RequestId)
        {
            var data = await _EmployeeService.DeleteClubCreationRequestAsync(RequestId);

            if (data.Status == 200)
                return Ok(data);

            return StatusCode(500, data);

        }
    }
}
