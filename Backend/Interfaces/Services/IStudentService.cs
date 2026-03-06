using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Services
{
    public interface IStudentService
    {
        public Task<ServiceResponseDto<StudentGetDto?>> GetAsync(int Id);
        public Task<ServiceResponseDto<StudentGetDto?>> UpdateImageAsync(int Id, IFormFile image);
        public Task<(byte[]? ImageData, string? ImageContentType)> GetImageAsync(int Id);
    }
}
