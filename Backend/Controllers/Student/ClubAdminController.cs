using EventsManagement.Dtos;
using EventsManagement.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsManagement.Controllers.Student
{
    [Authorize(Roles = "Student")]
    [Route("api/club/admin")]
    [ApiController]
    public class ClubAdminController(IClubAdminService _ClubAdminRepository) : ControllerBase
    {

        [HttpPost("event/{UserId}")]
        public async Task<IActionResult> RequestEventAsync(int UserId,ClubRequestEventDto form)
        {

            var data = await _ClubAdminRepository.RequestEventAsync(UserId, form);
            if (data.Status == 200)
            {
                return Ok(data);
            }
            else if (data.Status == 401)
                return Unauthorized(data);
            else
                return BadRequest(data);
           
        }

        [HttpPut("event/{UserId}")]
        public async Task<IActionResult> UpdateEventAsync(int UserId,UpdatePostDto form)
        {
         
            var data = await _ClubAdminRepository.UpdatePostAsync(UserId,form);
            if (data.Status == 200)
            {
                return Ok(data);
            }
            else if (data.Status == 401)
                return Unauthorized(data);
            else
                return BadRequest(data);

        }

        [HttpDelete("event/{UserId},{PostId}")]
        public async Task<IActionResult> DeleteEventAsync(int UserId,int PostId)
        {
       
            var data = await _ClubAdminRepository.DeletePostAsync(UserId, PostId);
            if (data.Status == 200)
            {
                return Ok(data);
            }
            else if (data.Status == 401)
                return Unauthorized(data);
            else
                return BadRequest(data);

        }


        [HttpPut("{UserId}")]
        public async Task<IActionResult> UpdateClubAsync(int UserId,ClubUpdateDto form)
        {
        
            var data = await _ClubAdminRepository.UpdateClubInfoAsync(UserId, form);
            if (data.Status == 200)
            {
                return Ok(data);
            }
            else if (data.Status == 401)
                return Unauthorized(data);
            else
                return BadRequest(data);

        }

        [HttpGet("candidates/{ClubId},{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetCandidatesApplicationsPaginatedAsync(int ClubId,int PageNumber,int PageSize)
        {

            var data = await _ClubAdminRepository.GetCandidateApplicationAsync(ClubId,PageNumber,PageSize);
            if (data.Status == 200)
            {
                return Ok(data);
            }
            else if (data.Status == 401)
                return Unauthorized(data);
            else
                return BadRequest(data);

        }

        [HttpPost("candidate/accept/{ApplicationId},{UserId},{ClubId}")]
        public async Task<IActionResult> AcceptCandidateApplicationAsync(int ApplicationId,int UserId, int ClubId)
        {

            var data = await _ClubAdminRepository.AcceptCandidateApplicationAsync(ApplicationId,UserId, ClubId);
            if (data.Status == 200)
            {
                return Ok(data);
            }
            else if (data.Status == 401)
                return Unauthorized(data);
            else
                return BadRequest(data);

        }

        [HttpPost("candidate/refuse/{ApplicationId}")]
        public async Task<IActionResult> RefuseCandidateApplicationAsync(int ApplicationId)
        {

            var data = await _ClubAdminRepository.RefuseCandidateApplicationAsync(ApplicationId);
            if (data.Status == 200)
            {
                return Ok(data);
            }
            else if (data.Status == 401)
                return Unauthorized(data);
            else
                return BadRequest(data);

        }

        [HttpPut("member/set-as-admin/{UserId},{ClubId}")]
        public async Task<IActionResult> SetAsAdminMemberAsync(int UserId, int ClubId)
        {

            var data = await _ClubAdminRepository.SetMemberAsAdminAsync(UserId, ClubId);
            if (data.Status == 200)
            {
                return Ok(data);
            }
            else if (data.Status == 401)
                return Unauthorized(data);
            else
                return BadRequest(data);

        }


        [HttpDelete("member/{UserId},{ClubId}")]
        public async Task<IActionResult> RemoveMemberAsync(int UserId, int ClubId)
        {

            var data = await _ClubAdminRepository.RemoveMemberAsync(UserId, ClubId);
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
