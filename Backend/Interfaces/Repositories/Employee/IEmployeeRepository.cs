using EventsManagement.Dtos;
using EventsManagement.Dtos.Employee;

namespace EventsManagement.Interfaces.Repositories.Employee
{
    public interface IEmployeeRepository
    {
        public Task<EmployeeInfoGetDto> GetInfoById(int Id);
        public Task<List<ClubsRequestsGetDto>> GetClubsRequestsAsync();
        public Task AcceptClubCreationRequest(int RequestId);
        public Task DeleteClubCreationRequest(int RequestId);
        public Task UpdateRefreshTokenAsync(string Token, int EmployeeId);
    }
}
