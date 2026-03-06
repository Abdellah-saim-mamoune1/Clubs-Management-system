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
           

                var club = await _db.Clubs.AsQueryable().FirstAsync(c => c.Id == form.ClubId);
                club.Name = form.Name;
                club.Description=form.Description;
                club.OpenForRegistrations = form.OpenForRegistrations;
                club.TypeId=form.TypeId;


                if (form.image == null)
                {
                    
                    club.ImageContentType = null;
                    club.ImageData = null;

                }

                else
                {
                    using var memoryStream = new MemoryStream();
                    await form.image.CopyToAsync(memoryStream);

                    club.ImageContentType = form.image.ContentType;
                    club.ImageData = memoryStream.ToArray();
                }

                 await _db.SaveChangesAsync();
                _cache.Remove("MostActiveClubs");
                _cache.Remove("NewClubs");
            
          
        }
        public async Task SetMemberAsAdminAsync(int UserId,int ClubId)
        {
            
            
                var user = await _db.UserClubs.AsQueryable().FirstAsync(u => u.ClubId == ClubId && u.UserId == UserId);
                user.Role = "Admin";

                await _db.SaveChangesAsync();
          
        }

        public async Task RemoveMemberAsync(int UserId, int ClubId)
        {
          
                var user = await _db.UserClubs.AsQueryable().FirstAsync(u => u.ClubId == ClubId && u.UserId == UserId);


                _db.Remove(user);
                await _db.SaveChangesAsync();
          
        }


        public async Task<ClubJoiningRequestPaginatedGetDto> GetCandidatesApplicationsPaginatedAsync(int ClubId,int PageNumber,int PageSize)
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
                   
                        StudentName=r.User!.FullName
                    })
                    .Skip((PageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();

                return new ClubJoiningRequestPaginatedGetDto { Applications=applications,TotalCount=Allapplications.Count()};
            
        }


        public async Task AcceptCandidateApplicationAsync(int ApplicationId,int StudentId, int ClubId)
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

        public async Task RefuseCandidateApplicationAsync(int ApplicationId)
        {

               
                var JoiningRequest = await _db.ClubJoiningRequests.FirstAsync(c => c.Id== ApplicationId);
                _db.Remove(JoiningRequest);
                await _db.SaveChangesAsync();

        }



        public async Task RequestEventAsync(ClubRequestEventDto form)
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


        
        public async Task DeletePostAsync(int Id)
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

        public async Task UpdatePostAsync(UpdatePostDto form)
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



                if (form.Image == null)
                {

                    Post.ImageContentType = null;
                    Post.ImageData = null;

                }

                else
                {
                    using var memoryStream = new MemoryStream();
                    await form.Image.CopyToAsync(memoryStream);

                    Post.ImageContentType = form.Image.ContentType;
                    Post.ImageData = memoryStream.ToArray();
                }


                _cache.Remove("UpcomingEvents");
                _cache.Remove("MostActiveClubs");
                _cache.Remove("NewClubs");
                await _db.SaveChangesAsync();
         
        }
    }
}
