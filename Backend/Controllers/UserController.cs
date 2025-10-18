using EventsManagement.Dtos;
using EventsManagement.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventsManagement.Controllers
{
   
    [Route("api/user")]
    [ApiController]
    public class UserController (IStudentService _UserService) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var data = await _UserService.GetAsync(int.Parse(Id));

            if(data.Status==200)
                return Ok(data);

            return BadRequest(data);
        }

        [Authorize]
        [HttpPut("image/")]
        public async Task<IActionResult> UpdateImageAsync(UserUpdateImageDto form)
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var data = await _UserService.UpdateImageAsync(int.Parse(Id),form.ImageUrl);

            if (data.Status == 200)
                return Ok(data);

            return BadRequest(data);
        }

        [HttpGet("by-id/{StudentId}")]
        public async Task<IActionResult> GetAsync(int StudentId)
        {
        
            var data = await _UserService.GetAsync(StudentId);

            if (data.Status == 200)
                return Ok(data);

            return BadRequest(data);
        }
    }
}
