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

        public async Task<ServiceResponseDto<StudentGetDto?>> UpdateImageAsync(int Id,string ImageUrl)
        {
            try
            {
                await _UserRepository.UpdateImageAsync(Id, ImageUrl);
                return new ServiceResponseDto<StudentGetDto?> { Status = 200 };
            }
            catch
            {
                return new ServiceResponseDto<StudentGetDto?> { Status = 500 };

            }
              

        }
    }
}
