
using EventsManagement.Dtos;
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

        [Authorize(Roles = "Employee")]
        [HttpGet("is-logged-in/")]
        public IActionResult IsEmployeeLoggedInAsync()
        {
            return Ok();
        }



        [HttpPost("login/")]
        public async Task<IActionResult> LoginEmployeeAsync(LoginEmployeeDto form)
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


        [Authorize(Roles = "Employee")]
        [HttpPost("clubs/requests/{RequestId}")]
        public async Task<IActionResult> AcceptCreationRequestAsync(int RequestId)
        {
            var data = await _EmployeeService.AcceptClubCreationRequestAsync(RequestId);

            if (data.Status == 200)
                return Ok(data);

            return StatusCode(500, data);

        }

        [Authorize(Roles = "Employee")]
        [HttpDelete("clubs/requests/{RequestId}")]
        public async Task<IActionResult> DeleteCreationRequestAsync(int RequestId)
        {
            var data = await _EmployeeService.DeleteClubCreationRequestAsync(RequestId);

            if (data.Status == 200)
                return Ok(data);

            return StatusCode(500, data);

        }



        [HttpGet("club-request/{id}/image")]
        public async Task<IActionResult> GetClubImage(int id)
        {
            var Image = await _EmployeeService.GetClubRequestImageAsync(id);

            if (Image.ImageData == null || Image.ImageContentType == null)
                return NotFound();

            return File(Image.ImageData, Image.ImageContentType);
        }


        [HttpGet("studets/")]
        public async Task<IActionResult> GetStudents()
        {
           return Ok( await _EmployeeService.GetStudentsAsync());
        }

    }
}
