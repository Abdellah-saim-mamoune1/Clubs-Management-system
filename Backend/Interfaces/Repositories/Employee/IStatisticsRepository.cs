using EventsManagement.Dtos.Employee;

namespace EventsManagement.Interfaces.Repositories.Employee
{
    public interface IStatisticsRepository
    {
        public Task<StatisticsGetDto> GetAsync();
    }
}
