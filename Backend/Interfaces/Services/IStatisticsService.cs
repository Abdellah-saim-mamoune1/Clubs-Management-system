using EventsManagement.Dtos;
using EventsManagement.Dtos.Employee;

namespace EventsManagement.Interfaces.Services
{
    public interface IStatisticsService
    {
        public  Task<ServiceResponseDto<StatisticsGetDto>> GetAsync();
    }
}
