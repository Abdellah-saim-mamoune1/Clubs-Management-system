using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Services.Employee
{
    public interface IEmployeeService
    {
        public Task<ServiceResponseDto<EmployeeInfoGetDto>> GetInfoByIdAsync(int Id);
        public Task<ServiceResponseDto<string>> LoginAsync(LoginEmployeeDto form);
        public Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> GetClubsRequestsAsync();
        public Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> AcceptClubCreationRequestAsync(int RequestId);
        public Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> DeleteClubCreationRequestAsync(int RequestId);
        public Task<(byte[]? ImageData, string? ImageContentType)> GetClubRequestImageAsync(int Id);
        public Task<ServiceResponseDto<List<StudentsGetDto>>> GetStudentsAsync();
    }
}
