using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Interfaces.Repositories.Student;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EventsManagement.Repositories.User
{
    public class StudentRepository(AppDbContext _db,IMemoryCache _cache) : IStudentRepository
    {
        public async Task<StudentGetDto?> GetAsync(int Id)
        {
            try
            {
                var UserClubs = await _db.UserClubs.Include(u => u.Club)
                    .ThenInclude(u => u!.ClubType).Where(u => u.UserId == Id).Select(
                    u => new UserClubsDto
                    {
                        Id = u.ClubId,
                        ImageUrl = u.Club!.ImageUrl,
                        Name = u.Club!.Name,
                        Type = u.Club.ClubType!.Type,
                        UserRole = u.Role
                    }).ToListAsync();

                return await _db.Users.Where(u => u.Id == Id).Select(u => new StudentGetDto
                {
                    Id = u.Id,
                    ImageUrl = u.ImageUrl,
                    Degree = u.Degree,
                    FullName = u.FullName,
                    YearOfDegree = u.YearOfDegree,
                    JoinedClubs = UserClubs,
                    Age = u.Age
                }).FirstAsync();
            }
            catch
            {
                throw;
            }
        }


        public async Task UpdateImageAsync(int Id,string ImageUrl)
        {
            try
            {
                var User = await _db.Users.FirstAsync(u => u.Id == Id);
                User.ImageUrl = ImageUrl;
                await _db.SaveChangesAsync();
                _cache.Remove("UpcomingEvents");
            }
            catch
            {
                throw;
            }
        }
    }
}
