using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Dtos.Employee;
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
            try
            {
                
                var data = await _EmployeeRepository.GetInfoById(Id);

                return new ServiceResponseDto<EmployeeInfoGetDto> { Data = data, Status = 200 };

            }
            catch
            {
                return new ServiceResponseDto<EmployeeInfoGetDto> { Status = 500 };
            }


        }


        public async Task<ServiceResponseDto<string>> LoginAsync(Dtos.Employee.LoginEmployeeDto form)
        {
            try
            {
                int EmployeeId = await DoesEmployeeExist(form);
                if (EmployeeId == -1)
                {
                    return new ServiceResponseDto<string> { Status = 401 };
                }

                 var Helper = new GenerateKeys(_Configuration);

                 string Token = Helper.CreateEmployeeToken(EmployeeId);

                 await _EmployeeRepository.UpdateRefreshTokenAsync(Token,EmployeeId);

                return new ServiceResponseDto<string> {Data=Token, Status = 200 };

            }
            catch
            {
                return new ServiceResponseDto<string> { Status = 500 };
            }


        }

        async Task<int> DoesEmployeeExist(Dtos.Employee.LoginEmployeeDto form)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(e => (e.Account == form.Account )&& (e.HashedPassword == form.Password));
            return employee == null ? -1 : employee.Id;
        }

        public async Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> GetClubsRequestsAsync()
        {
            try
            {

                var data = await _EmployeeRepository.GetClubsRequestsAsync();

                return new ServiceResponseDto<List<ClubsRequestsGetDto>> { Data = data, Status = 200 };

            }
            catch
            {
                return new ServiceResponseDto<List<ClubsRequestsGetDto>> { Status = 500 };
            }


        }

        public async Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> AcceptClubCreationRequestAsync(int RequestId)
        {
            try
            {

                 await _EmployeeRepository.AcceptClubCreationRequest(RequestId);

                 return new ServiceResponseDto<List<ClubsRequestsGetDto>> { Status = 200 };

            }
            catch
            {
                return new ServiceResponseDto<List<ClubsRequestsGetDto>> { Status = 500 };
            }

        }


        public async Task<ServiceResponseDto<List<ClubsRequestsGetDto>>> DeleteClubCreationRequestAsync(int RequestId)
        {
            try
            {

                await _EmployeeRepository.DeleteClubCreationRequest(RequestId);

                return new ServiceResponseDto<List<ClubsRequestsGetDto>> { Status = 200 };

            }
            catch
            {
                return new ServiceResponseDto<List<ClubsRequestsGetDto>> { Status = 500 };
            }

        }
    }
}
