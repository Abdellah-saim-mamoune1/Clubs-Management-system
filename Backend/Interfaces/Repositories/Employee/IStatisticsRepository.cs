using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Repositories.Employee
{
    public interface IStatisticsRepository
    {
        public Task<StatisticsGetDto> GetAsync();
    }
}
