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
           
                var userClubs = await _db.UserClubs
     
                .Where(u => u.UserId == Id && u.Club != null)
                
                .Select(u => new UserClubsDto
                 {
                   Id = u.ClubId,
                
                   Name = u.Club!.Name,
                   Type =  u.Club!.ClubType!.Type,
                   UserRole = u.Role
                 })
                .ToListAsync();

                return await _db.Users.Where(u => u.Id == Id).Select(u => new StudentGetDto
                {
                    Id = u.Id,
                    HasImage = u.ImageData==null?false:true,
                    Degree = u.Degree,
                    FullName = u.FullName,
                    YearOfDegree = u.YearOfDegree,
                    JoinedClubs = userClubs,
                    Age = u.Age
                }).FirstOrDefaultAsync();
           
        }


        public async Task UpdateImageAsync(int Id,string ContentType, byte[] ImageData)
        {
       
                var User = await _db.Users.FirstAsync(u => u.Id == Id);
                User.ImageContentType = ContentType;
                User.ImageData = ImageData;


                await _db.SaveChangesAsync();
                _cache.Remove("UpcomingEvents");
       
        }


        public async Task<(byte[]? ImageData,string? ImageContentType)> GetImageAsync(int Id)
        {
       
                var User = await _db.Users.FirstAsync(u => u.Id == Id);
                return (User.ImageData, User.ImageContentType);
        
        }
    }
}
