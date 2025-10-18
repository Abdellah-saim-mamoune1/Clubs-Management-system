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
                    EventsManagement.Classes.Club club = new EventsManagement.Classes.Club
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


        public async Task UpdateRefreshTokenAsync(string Token,int EmployeeId)
        {
            try
            {
                var Employee = await _db.Employees.FirstAsync(e => e.Id == EmployeeId);

                Employee.RefreshToken = Token;
                Employee.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30);

                await _db.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
            
        }


    }
}
