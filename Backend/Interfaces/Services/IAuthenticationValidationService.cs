using EventsManagement.Classes;
using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Services
{
    public interface IAuthenticationValidationService
    {
      
        public Task<User?> ValidateRefreshEmployeeTokens(RefreshTokenRequestDto request);
        public Task<User?> ValidateRefreshClientTokens(RefreshTokenRequestDto request);
    }
}
