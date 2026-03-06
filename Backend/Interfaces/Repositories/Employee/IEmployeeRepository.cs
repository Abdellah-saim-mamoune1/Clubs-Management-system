using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Repositories.Employee
{
    public interface IEmployeeRepository
    {
        public Task<EmployeeInfoGetDto> GetInfoById(int Id);
        public Task<List<ClubsRequestsGetDto>> GetClubsRequestsAsync();
        public Task AcceptClubCreationRequest(int RequestId);
        public Task DeleteClubCreationRequest(int RequestId);
        public Task<(byte[]? ImageData, string? ImageContentType)> GetClubRequestImageAsync(int Id);
        public Task<List<StudentsGetDto>> GetStudentsAsync();

        // For sample
        public Task ClubTypeCreationForSample(string Type);
        public Task ClubCreationforSample(int StudentId, Classes.Club club);
    }
}
