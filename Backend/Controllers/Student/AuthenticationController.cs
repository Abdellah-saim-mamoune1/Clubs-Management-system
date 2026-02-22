using EventsManagement.Dtos;
using EventsManagement.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsManagement.Controllers.Student
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService _Authenticate) : ControllerBase
    {


        [HttpPost("login")]
        public async Task<ActionResult?> Login(LoginDto login)
        {
            var result = await _Authenticate.LoginAsync(login);

            if (result.Status == 401)
                return Unauthorized("Invalid Credentials");

            else if (result.Status == 500)
                return StatusCode(500, "Internal server error.");

            return Ok(result.Data);
        }

        [Authorize]
        [HttpPost("is-logged-in")]
        public ActionResult IsLoggedIn()
        {
            return Ok();
        }

    }
}
