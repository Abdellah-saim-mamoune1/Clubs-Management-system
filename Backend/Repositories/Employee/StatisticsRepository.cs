using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Interfaces.Repositories.Employee;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.Repositories.Employee
{
    public class StatisticsRepository(AppDbContext _db) : IStatisticsRepository
    {


        public async Task<StatisticsGetDto> GetAsync()
        {

                var StudentsCount = await _db.Users.CountAsync();
                var ClubsCount = await _db.Clubs.CountAsync();
                var EventsCount = await _db.Events.CountAsync();
                var lastMonthNewClubs = await _db.Clubs
                     .Where(c => c.CreatedAt >= DateTime.UtcNow.AddMonths(-1))
                     .OrderByDescending(c => c.CreatedAt)
                     .CountAsync();

                return new StatisticsGetDto
                {
                    StudentsCount = StudentsCount,
                    ClubsCount = ClubsCount,
                    EventsCount = EventsCount,
                    LastMonthNewClubs = lastMonthNewClubs
                };
        }


    }
}
