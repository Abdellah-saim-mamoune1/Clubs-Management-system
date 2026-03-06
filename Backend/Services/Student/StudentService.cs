using EventsManagement.Dtos;
using EventsManagement.Interfaces.Repositories.Student;
using EventsManagement.Interfaces.Services;

namespace EventsManagement.Services.User
{
    public class StudentService(IStudentRepository _UserRepository): IStudentService
    {
        public async Task<ServiceResponseDto<StudentGetDto?>> GetAsync(int Id)
        {

            var data = await _UserRepository.GetAsync(Id);

            if (data == null)
                return new ServiceResponseDto<StudentGetDto?> { Status = 500 };

            return new ServiceResponseDto<StudentGetDto?> { Data = data, Status = 200 };

        }

        public async Task<ServiceResponseDto<StudentGetDto?>> UpdateImageAsync(int Id,IFormFile image)
        {
      
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);

                await _UserRepository.UpdateImageAsync(Id, image.ContentType, memoryStream.ToArray());

                return new ServiceResponseDto<StudentGetDto?> { Status = 200 };
            

        }


        public async Task<(byte[]? ImageData, string? ImageContentType)> GetImageAsync(int Id)
        {
          
                return  await _UserRepository.GetImageAsync(Id) ;
            
        }
    }
}
