using EventsManagement.Classes;
using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Repositories.Authentication
{
    public interface IAuthenticationRepository
    {

        public Task<(string? uuid, string? token)> LoginStudentAsync(LoginDto form);
        public Task Registre(string uuid, string token);

    }
}
