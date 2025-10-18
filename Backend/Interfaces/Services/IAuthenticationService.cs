using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public Task<ServiceResponseDto<TokenResponseDto?>> LoginAsync(LoginDto form);


    }

}
