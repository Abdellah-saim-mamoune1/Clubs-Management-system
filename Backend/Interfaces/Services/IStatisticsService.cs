using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Services
{
    public interface IStatisticsService
    {
        public  Task<ServiceResponseDto<StatisticsGetDto>> GetAsync();
    }
}
