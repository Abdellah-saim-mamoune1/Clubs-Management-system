using EventsManagement.Dtos;
using EventsManagement.Interfaces.Repositories.Employee;
using EventsManagement.Interfaces.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventsManagement.Services.Employee
{
    public class StatisticsService(IStatisticsRepository _StatisticsRepository) : IStatisticsService
    {

        public async Task<ServiceResponseDto<StatisticsGetDto>> GetAsync()
        {
            
                var data = await _StatisticsRepository.GetAsync();

                 return new ServiceResponseDto<StatisticsGetDto> {Data=data, Status = 200 };

        
        }



    }
}
