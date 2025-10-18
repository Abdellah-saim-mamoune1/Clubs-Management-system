using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Helpers;
using EventsManagement.Interfaces.Repositories.Authentication;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.Services.Student
{
    public class AuthenticationService
       (
       IAuthenticationRepository _AuthenticationRepository,
       AppDbContext _db,
       IConfiguration _Configuration
      
       ) : Interfaces.Services.IAuthenticationService
    {

        public async Task<ServiceResponseDto<TokenResponseDto?>> LoginAsync(LoginDto form)
        {
            try
            {
                var data = await _AuthenticationRepository.LoginStudentAsync(form);
                if (data.uuid == null || data.token == null)
                    return new ServiceResponseDto<TokenResponseDto?> { Status = 401 };

                int UserId = await GetUserIdAsync(data.uuid);
                if (UserId==-1)
                {
                    await _AuthenticationRepository.Registre(data.uuid, data.token);
                }

                var Helper = new GenerateKeys(_Configuration);

                string Token= Helper.CreateStudentToken(UserId);
                return new ServiceResponseDto<TokenResponseDto?>
                {
                    Data = new TokenResponseDto
                    {
                        Token = Token,
                        Uuid = Guid.Parse(data.uuid)
                    },
                    Status = 200
                };

            }
            catch
            {
                return new ServiceResponseDto<TokenResponseDto?> { Status = 401 };
            }
        }

        public async Task<int> GetUserIdAsync(string Uuid)
        {
            var user=await _db.Users.FirstOrDefaultAsync(u=>u.Uuid == Guid.Parse(Uuid));
            return user == null ? -1 : user.Id;

        }

       

    }
}








