using EventsManagement.Classes;
using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Interfaces.Repositories.Student;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EventsManagement.Repositories.Club
{
    public class ClubAdminRepository(AppDbContext _db,IMemoryCache _cache) : IClubAdminRepository
    {


        public async Task UpdateClubInfoAsync(ClubUpdateDto form)
        {
            try
            {
                var club = await _db.Clubs.AsQueryable().FirstAsync(c => c.Id == form.ClubId);
                club.Name = form.Name;
                club.Description=form.Description;
                club.OpenForRegistrations = form.OpenForRegistrations;
                club.TypeId=form.TypeId;
                club.ImageUrl=form.ImageUrl;
                await _db.SaveChangesAsync();
                _cache.Remove("MostActiveClubs");
                _cache.Remove("NewClubs");
            }
            catch
            {
                throw;
            }
        }
        public async Task SetMemberAsAdminAsync(int UserId,int ClubId)
        {
            try
            {
                var user = await _db.UserClubs.AsQueryable().FirstAsync(u => u.ClubId == ClubId && u.UserId == UserId);
                user.Role = "Admin";

                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveMemberAsync(int UserId, int ClubId)
        {
            try
            {
                var user = await _db.UserClubs.AsQueryable().FirstAsync(u => u.ClubId == ClubId && u.UserId == UserId);


                _db.Remove(user);
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }


        public async Task<ClubJoiningRequestPaginatedGetDto> GetCandidatesApplicationsPaginatedAsync(int ClubId,int PageNumber,int PageSize)
        {
            try
            {
                var Allapplications =  _db.ClubJoiningRequests.Include(r => r.User).AsQueryable();
                var applications=await Allapplications.
                    Where(r => r.ClubId == ClubId).Select(r=>new ClubJoiningRequestGetDto
                    {
                        ApplicationId=r.Id,
                        StudentEmail = r.UserEmail,
                        StudentMotivation = r.Motivation,
                        Date = r.CreatedAt,
                        StudentId=r.StudentId,
                        StudentImageUrl=r.User!.ImageUrl,
                        StudentName=r.User.FullName
                    })
                    .Skip((PageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();

                return new ClubJoiningRequestPaginatedGetDto { Applications=applications,TotalCount=Allapplications.Count()};
            }
            catch
            {
                throw;
            }
        }


        public async Task AcceptCandidateApplicationAsync(int ApplicationId,int StudentId, int ClubId)
        {
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    var user = new UserClub { ClubId = ClubId, UserId = StudentId, Role = "Member" };
                    _db.Add(user);
                    await _db.SaveChangesAsync();
                    await RefuseCandidateApplicationAsync(ApplicationId);
                    await transaction.CommitAsync();
                });
             
            }
            catch
            {
                throw;
            }
        }

        public async Task RefuseCandidateApplicationAsync(int ApplicationId)
        {
            try
            {

               
                var JoiningRequest = await _db.ClubJoiningRequests.FirstAsync(c => c.Id== ApplicationId);
                _db.Remove(JoiningRequest);
                await _db.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
        }



        public async Task RequestEventAsync(ClubRequestEventDto form)
        {

            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                   
                 
                      
                            EventRegistration eventRegistration = new EventRegistration
                            {
                                MaxRegistrationsCount = form.MaxRegistrationsCount,
                                CurrentRegistrationsCount = 0,
                            };

                            _db.Add(eventRegistration);
                            await _db.SaveChangesAsync();
                       

                         Event Event = new Event
                         {
                         Address = form.Address,
                         CreatedAt = DateTime.UtcNow,
                         Description = form.Content,
                         Date = form.Date,
                         Title = form.Title,
                         ClubId = form.ClubId,
                         From = form.From,
                         To = form.To,
                         IsPrivate = form.IsPrivate,
                         RegistrationId = eventRegistration.Id
                         };
                        
                        _db.Add(Event);
                        await _db.SaveChangesAsync();
                       _cache.Remove("UpcomingEvents");
                       _cache.Remove("MostActiveClubs");
                       _cache.Remove("NewClubs");
                    await transaction.CommitAsync();
                    
                });

            }
            catch { throw; }

        }


        
        public async Task DeletePostAsync(int Id)
        {
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    var Event = await _db.Events.FirstAsync(c => c.Id == Id);
                    var UsersEvent = await _db.UserEvents.Where(u => u.EventId == Id).ToListAsync();

                    _db.RemoveRange(UsersEvent);
                    _db.Remove(Event);
                    if (Event.RegistrationId != null)
                    {
                        var Registration = await _db.EventsRegistrations.FirstAsync(r => r.Id == Event.RegistrationId);
                        _db.Remove(Registration);
                     
                    }

                    await _db.SaveChangesAsync();
                    _cache.Remove("UpcomingEvents");
                    _cache.Remove("MostActiveClubs");
                    _cache.Remove("NewClubs");
                    await transaction.CommitAsync();
                });
            }
            catch { throw; }

        }

        public async Task UpdatePostAsync(UpdatePostDto form)
        {
            try
            {
                var Post = await _db.Events.Include(e=>e.EventRegistration).FirstAsync(c => c.Id == form.Id);
                Post.Description = form.Content;
                Post.Title=form.Title;
                Post.From = form.From;
                Post.Date = form.Date;
                Post.Address = form.Address;
                Post.IsPrivate = form.IsPrivate;
                Post.To=form.To;
                Post.EventRegistration!.MaxRegistrationsCount = form.MaxRegistrationCount;
                _cache.Remove("UpcomingEvents");
                await _db.SaveChangesAsync();
            }
            catch { throw; }

        }
    }
}
