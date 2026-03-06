using EventsManagement.Dtos;
using EventsManagement.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventsManagement.Controllers.Student
{
   
    [Route("api/user")]
    [ApiController]
    public class UserController (IStudentService _UserService) : ControllerBase
    {
        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var data = await _UserService.GetAsync(int.Parse(Id));

            if(data.Status==200)
                return Ok(data);

            return BadRequest(data);
        }

        [Authorize(Roles = "Student")]
        [HttpPut("image/")]
        public async Task<IActionResult> UpdateImageAsync(IFormFile image)
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var data = await _UserService.UpdateImageAsync(int.Parse(Id), image);

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



        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetStudentImage(int id)
        {
            var Image = await _UserService.GetImageAsync(id);

            if (Image.ImageData == null||Image.ImageContentType==null)
                return NotFound();

            return File(Image.ImageData, Image.ImageContentType);
        }
    }
}
