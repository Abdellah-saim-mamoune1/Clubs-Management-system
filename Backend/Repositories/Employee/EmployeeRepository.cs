using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Interfaces.Repositories.Employee;
using Microsoft.EntityFrameworkCore;
using EventsManagement.Classes;
using Microsoft.Extensions.Caching.Memory;


namespace EventsManagement.Repositories.Employee
{
    public class EmployeeRepository(AppDbContext _db, IMemoryCache _cache) : IEmployeeRepository
    {


        public async Task<EmployeeInfoGetDto> GetInfoById(int Id)
        {
           
                return await _db.Employees.Where(e => e.Id == Id)
                    .Select(e => new EmployeeInfoGetDto
                    {
                        Account = e.Account,
                        Id = e.Id,
                        Name = e.Name,

                    })
                    .FirstAsync();
       
        }

        public async Task<List<ClubsRequestsGetDto>> GetClubsRequestsAsync()
        {
         
                return await _db.RequestedClubs
                    .Select(c => new ClubsRequestsGetDto
                    {
                        StudentId=c.StudentId,
                        ClubName=c.ClubName,
                        ClubTypeId=c.ClubTypeId,
                        Description=c.Description,
                        CreatedAt=c.CreatedAt,
                        RequestId=c.Id

                    })
                    .ToListAsync();
        }

        public async Task AcceptClubCreationRequest(int RequestId)
        {
            
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    var request = await _db.RequestedClubs.FirstAsync(r => r.Id == RequestId);
                    Classes.Club club = new Classes.Club
                    {

                        Name = request.ClubName,
                        ImageContentType = request.ImageContentType,
                        ImageData= request.ImageData,
                        Description = request.Description,
                        TypeId = request.ClubTypeId,
                       
                    };

                    _db.Add(club);
                    await _db.SaveChangesAsync();

                    var Admin = new UserClub { ClubId = club.Id, Role = "Admin", UserId = request.StudentId };

                    _db.UserClubs.Add(Admin);
                    await _db.SaveChangesAsync();

                    await DeleteClubCreationRequest(request.Id);

                    _cache.Remove("MostActiveClubs");
                    _cache.Remove("NewClubs");

                    await transaction.CommitAsync();

                });
         
        }

        public async Task<List<StudentsGetDto>> GetStudentsAsync()
        {
                return await _db.Users
                    .Select(c => new StudentsGetDto
                    {
                     Id=c.Id,
                     Age=c.Age,
                     Degree=c.Degree,
                     FullName=c.FullName
                    })
                    .ToListAsync();
          
        }

        

        // For sample
        public async Task ClubTypeCreationForSample(string Type)
        {
            _db.ClubTypes.Add(new ClubType { Type = Type });
            await _db.SaveChangesAsync();
        }


        // For sample
        public async Task ClubCreationforSample(int StudentId, Classes.Club club)
        {
           
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    
                    
                    _db.Add(club);
                    await _db.SaveChangesAsync();

                    var Admin = new UserClub { ClubId = club.Id, Role = "Admin", UserId = StudentId };

                    _db.UserClubs.Add(Admin);
                    await _db.SaveChangesAsync();

                 
                    await transaction.CommitAsync();

                });
        }


        public async Task DeleteClubCreationRequest(int RequestId)
        {
           
                var request = await _db.RequestedClubs.FirstAsync(r => r.Id == RequestId);

                _db.Remove(request);
                await _db.SaveChangesAsync();

          
        }



        public async Task<(byte[]? ImageData, string? ImageContentType)> GetClubRequestImageAsync(int Id)
        {

            var Club = await _db.RequestedClubs.FirstAsync(u => u.Id == Id);
            return (Club.ImageData, Club.ImageContentType);

        }

    }
}
