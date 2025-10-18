using EventsManagement.Dtos;
using EventsManagement.Dtos.Employee;

namespace EventsManagement.Interfaces.Services.Employee
{
    public interface IEmployeeService
    {
        public Task<ServiceResponseDto<EmployeeInfoGetDto>> GetInfoByIdAsync(int Id);
        public Task<ServiceResponseDto<string>> LoginAsync(Dtos.Employee.LoginEmployeeDto form);
        public Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> GetClubsRequestsAsync();
        public Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> AcceptClubCreationRequestAsync(int RequestId);
        public Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> DeleteClubCreationRequestAsync(int RequestId);
    }
}
