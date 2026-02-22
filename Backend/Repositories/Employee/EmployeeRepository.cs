using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Dtos.Employee;
using EventsManagement.Interfaces.Repositories.Employee;
using Microsoft.EntityFrameworkCore;
using EventsManagement.Classes;


namespace EventsManagement.Repositories.Employee
{
    public class EmployeeRepository(AppDbContext _db) : IEmployeeRepository
    {


        public async Task<EmployeeInfoGetDto> GetInfoById(int Id)
        {
            try
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
            catch
            {
                throw;
            }
        }

        public async Task<List<ClubsRequestsGetDto>> GetClubsRequestsAsync()
        {
            try
            {
                return await _db.RequestedClubs
                    .Select(c => new ClubsRequestsGetDto
                    {
                        StudentId=c.StudentId,
                        ClubName=c.ClubName,
                        ClubTypeId=c.ClubTypeId,
                        ImageUrl=c.ImageUrl,
                        CreatedAt=c.CreatedAt

                    })
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AcceptClubCreationRequest(int RequestId)
        {
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    var request = await _db.RequestedClubs.FirstAsync(r => r.Id == RequestId);
                    Classes.Club club = new Classes.Club
                    {
                        Id = RequestId,
                        Name = request.ClubName,
                        ImageUrl = request.ImageUrl,
                        TypeId = request.ClubTypeId,

                    };
                    _db.Add(club);
                    await _db.SaveChangesAsync();

                    var Admin = new UserClub { ClubId = club.Id, Role = "Admin", UserId = request.StudentId };

                    _db.UserClubs.Add(Admin);
                    await _db.SaveChangesAsync();

                    await DeleteClubCreationRequest(request.Id);
                    await transaction.CommitAsync();

                });
            }
            catch
            {
                throw;
            }
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
            try
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
            catch
            {
                throw;
            }
        }


        public async Task DeleteClubCreationRequest(int RequestId)
        {
            try
            {
                var request = await _db.RequestedClubs.FirstAsync(r => r.Id == RequestId);

                _db.Remove(request);
                await _db.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
        }


   


    }
}
