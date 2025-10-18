using EventsManagement.Dtos;
using EventsManagement.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



namespace EventsManagement.Controllers
{
    [Route("api/clubs")]
    [ApiController]
    public class ClubUserController(IClubUserService _ClubUserService) : ControllerBase
    {
        [HttpGet("search-by-name")]
        public async Task<IActionResult> GetClubsPaginatedAsync(
            [FromQuery] int pageNumber,
            [FromQuery]  int pageSize,
            [FromQuery] string? name = "")
        {

            var data = await _ClubUserService.SearchClubsPaginatedAsync(name,pageNumber, pageSize);
            if (data.Status == 200)
            {
                return Ok(data);
            }


            else
                return BadRequest(data);
        }

        [Authorize]
        [HttpPost("request-creating/")]
        public async Task<IActionResult> RequestClubCreationAsync(ClubRequestCreationDto form)
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var data = await _ClubUserService.RequestClubCreationAsync(int.Parse(Id),form);
            if (data.Status == 200)
            {
                return Ok(data);
            }
            else
                return BadRequest(data);
        }

        [Authorize]
        [HttpGet("check-requested-club-creation/")]
        public async Task<IActionResult> CheckRequestClubCreationAsync()
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var data = await _ClubUserService.CheckRequestClubCreationAsync(int.Parse(Id));
            if (data.Status == 200)
            {
                return Ok(data);
            }
            else
                return StatusCode(500,data);
        }

        [HttpPost("join/")]
        public async Task<IActionResult> GetClubsPaginatedAsync(ClubJoiningRequestSetDto form)
        {
           
            var data = await _ClubUserService.JoinClubRequestAsync(form);
            if (data.Status == 200)
            {
                return Ok(data);
            }
            else
                return BadRequest(data);
        }
    
        [HttpGet("new/")]
        public async Task<IActionResult> GetNewClubsAsync()
        {

            var data = await _ClubUserService.GetNewClubsAsync();
            if (data.Status == 200)
            {
                return Ok(data);
            }

     
            else
                return BadRequest(data);
        }

        [HttpGet("most-active/")]
        public async Task<IActionResult> GetMostActiveClubsAsync()
        {

            var data = await _ClubUserService.GetMostActiveClubsAsync();
            if (data.Status == 200)
            {
                return Ok(data);
            }

     
            else
                return BadRequest(data);
        }

        [HttpGet("members/{ClubId}")]
        public async Task<IActionResult> GetClubsMembersAsync(int ClubId)
        {
          
            var data = await _ClubUserService.GetClubsMembersAsync(ClubId);
            if (data.Status == 200)
            {
                return Ok(data);
            }

      

            else
                return BadRequest(data);
        }

        [HttpGet("events/{UserId},{ClubId}")]
        public async Task<IActionResult> GetClubEventsPaginatedAsync(int UserId,int ClubId)
        {
        


            var data = await _ClubUserService.GetClubEventsPaginatedAsync(UserId, ClubId);
            if (data.Status == 200)
            {
                return Ok(data);
            }

            else if (data.Status == 401)
                return Unauthorized(data);

            else
                return BadRequest(data);
        }

       

        [HttpGet("events/search-by-name")]
        public async Task<IActionResult> GetEventsPaginatedAsync([FromQuery] string? name = "",
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
       
            var data = await _ClubUserService.SearchEventsByNameAsync(name,pageNumber,pageSize);
            if (data.Status == 200)
            {
                return Ok(data);
            }

            else if (data.Status == 401)
                return Unauthorized(data);

            else
                return StatusCode(500, data);
        }

        [HttpGet("events/upcoming")]
        public async Task<IActionResult> GetUpcomingClubsEventsPaginatedAsync()
        {
            int Id = -1;
            var Val = User.FindFirst(ClaimTypes.NameIdentifier);
            if (Val != null)
                Id = int.Parse(Val.Value);


            var data = await _ClubUserService.GetUpcomingEventsAsync(Id);
            if (data.Status == 200)
            {
                return Ok(data);
            }

            else if (data.Status == 401)
                return Unauthorized(data);

            else
                return BadRequest(data);
        }

        [HttpGet("by-type/")]
        public async Task<IActionResult> GetClubsByTypeAsync([FromQuery] int typeId, [FromQuery] string? name = "")
        {

            var data = await _ClubUserService.GetClubsByTypeAsync(typeId,name);
            if (data.Status == 200)
            {
                return Ok(data);
            }

            else if (data.Status == 401)
                return Unauthorized(data);

            else
                return BadRequest(data);
        }


        [HttpGet("by-Id/{UserId},{Id}")]
        public async Task<IActionResult> GetClubByIdAsync(int UserId, int Id)
        {

            var data = await _ClubUserService.GetClubByIdAsync(UserId,Id);
            if (data.Status == 200)
            {
                return Ok(data);
            }

            else if (data.Status == 401)
                return Unauthorized(data);

            else
                return BadRequest(data);
        }

       


        [HttpGet("types/")]
        public async Task<IActionResult> GetClubsTypesAsync()
        {

            var data = await _ClubUserService.GetClubsTypesAsync();
            if (data.Status == 200)
            {
                return Ok(data);
            }

            else if (data.Status == 401)
                return Unauthorized(data);

            else
                return BadRequest(data);
        }

        [HttpPost("event/{EventId}/view")]
        public async Task<IActionResult> ViewEventsAsync(int EventId)
        {

            var data = await _ClubUserService.ViewEventsAsync(EventId);
            if (data.Status == 200)
            {
                return Ok(data);
            }

            else if (data.Status == 401)
                return Unauthorized(data);

            else
                return BadRequest(data);
        }

        [Authorize]
        [HttpPost("event/{EventId}/join")]
        public async Task<IActionResult> JoinAnEventAsync(int EventId)
        {
           
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
          
            var data = await _ClubUserService.JoinAnEventAsync(int.Parse(Id),EventId);
            if (data.Status == 200)
            {
                return Ok(data);
            }

            else if (data.Status == 401)
            {
              
                return Unauthorized("data");
            }
            else
                return BadRequest(data);
        }

        [HttpGet("event/{UserId},{EventId}")]
        public async Task<IActionResult> GetEventByIdAsync(int UserId, int EventId)
        {
          
            var data = await _ClubUserService.GetEventByIdAsync(UserId,EventId);
            if (data.Status == 200)
            {
                return Ok(data);
            }

            else if (data.Status == 401)
                return Unauthorized(data);

            else
                return BadRequest(data);
        }

    }
}
