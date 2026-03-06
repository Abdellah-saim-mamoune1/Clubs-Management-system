using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Helpers;
using EventsManagement.Interfaces.Repositories.Employee;
using EventsManagement.Interfaces.Services.Employee;
using Microsoft.EntityFrameworkCore;


namespace EventsManagement.Services.Employee
{
    public class EmployeeService(
        IEmployeeRepository _EmployeeRepository,
        IConfiguration _Configuration,
        AppDbContext _db): IEmployeeService
    {
        public async Task<ServiceResponseDto<EmployeeInfoGetDto>> GetInfoByIdAsync(int Id)
        {
         
                var data = await _EmployeeRepository.GetInfoById(Id);

                return new ServiceResponseDto<EmployeeInfoGetDto> { Data = data, Status = 200 };

        }


        public async Task<ServiceResponseDto<string>> LoginAsync(LoginEmployeeDto form)
        {
  
                int EmployeeId = await DoesEmployeeExist(form);
                if (EmployeeId == -1)
                {
                    return new ServiceResponseDto<string> { Status = 401 };
                }

                 var Helper = new GenerateKeys(_Configuration);

                 string Token = Helper.CreateEmployeeToken(EmployeeId);

               

                return new ServiceResponseDto<string> {Data=Token, Status = 200 };


        }

        async Task<int> DoesEmployeeExist(LoginEmployeeDto form)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(e => (e.Account == form.Account )&& (e.Password == form.Password));
            return employee == null ? -1 : employee.Id;
        }

        public async Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> GetClubsRequestsAsync()
        {
 
                var data = await _EmployeeRepository.GetClubsRequestsAsync();

                return new ServiceResponseDto<List<ClubsRequestsGetDto>> { Data = data, Status = 200 };


        }

        public async Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> AcceptClubCreationRequestAsync(int RequestId)
        {
           
                 await _EmployeeRepository.AcceptClubCreationRequest(RequestId);

                 return new ServiceResponseDto<List<ClubsRequestsGetDto>> { Status = 200 };

        }


        public async Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> DeleteClubCreationRequestAsync(int RequestId)
        {
   
                await _EmployeeRepository.DeleteClubCreationRequest(RequestId);

                return new ServiceResponseDto<List<ClubsRequestsGetDto>> { Status = 200 };


        }


        public async Task<ServiceResponseDto<List<StudentsGetDto>>> GetStudentsAsync()
        {
           

                var data=await _EmployeeRepository.GetStudentsAsync();

                return new ServiceResponseDto<List<StudentsGetDto>> { Status = 200 ,Data=data};

         

        }




        public async Task<(byte[]? ImageData, string? ImageContentType)> GetClubRequestImageAsync(int Id)
        {

            return await _EmployeeRepository.GetClubRequestImageAsync(Id);

        }

    }
}
