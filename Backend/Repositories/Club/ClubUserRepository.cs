using EventsManagement.Classes;
using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Interfaces.Repositories.Student;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EventsManagement.Repositories.Club
{
    public class ClubUserRepository(AppDbContext _db,IMemoryCache _cache) : IClubUserRepository
    {
        public async Task JoiningClubRequest(ClubJoiningRequestSetDto form)
        {
            try
            {

                ClubJoiningRequest request = new ClubJoiningRequest
                {
                    StudentId = form.StudentId,
                    UserEmail = form.Email,
                    Motivation = form.Motivation,
                    ClubId = form.ClubId
                };

                _db.Add(request);
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

        }

        public async Task RequestClubCreationAsync(int StudentId,ClubRequestCreationDto form)
        {
            try
            {

                RequestedClub request = new RequestedClub
                { 
                   StudentId=StudentId,
                   ClubName=form.ClubName,
                   ClubTypeId=form.ClubTypeId,
                   ImageUrl=form.ImageUrl
                };

                _db.Add(request);
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

        }

        

        public async Task<List<EventsGetDto>> GetUpcomingEventsAsync(int UserId)
        {
            string cacheKey = $"UpcomingEvents";
            if (_cache.TryGetValue(cacheKey, out List<EventsGetDto>? cachedEvents))
                return cachedEvents!;

            var events = await _db.Events
                .Include(e => e.Club)
                .Include(e => e.EventRegistration)
                .Where(c => c.Date > DateOnly.FromDateTime(DateTime.UtcNow))
                .Select(e => new EventsGetDto
                {
                    Address = e.Address,
                    Id = e.Id,
                    Content = e.Description,
                    Date = e.Date,
                    From = e.From,
                    To = e.To,
                    Title = e.Title,
                    Views = e.Views,
                    ClubId = e.ClubId,
                    ClubName = e.Club!.Name,
                    IsStudentJoined = e.UserEvents.Any(U => U.UserId == UserId && U.EventId == e.Id),
                    RegistrationInfo = e.EventRegistration == null ? null : new RegistrationInfoDto
                    {
                        CurrentRegistrationsCount = e.EventRegistration.CurrentRegistrationsCount,
                        MaxRegistrationsCount = e.EventRegistration.MaxRegistrationsCount
                    },
                    IsPrivate = e.IsPrivate
                }).ToListAsync();
                 events.Reverse();
            _cache.Set(cacheKey, events);

            return events;
        }


        public async Task<List<ClubInfoGetDto>> GetNewClubsAsync()
        {
            const string cacheKey = "NewClubs";

            if (_cache.TryGetValue(cacheKey, out List<ClubInfoGetDto>? cachedClubs))
            {
              
                return cachedClubs!;
            }


            var clubs = await _db.Clubs
            .Include(c => c.ClubType)
             .Include(c => c.Events)
             .Include(c => c.UserClubs)
              .OrderByDescending(c => c.Id) 
               .Select(c => new ClubInfoGetDto
             {
           Id = c.Id,
           ImageUrl = c.ImageUrl,
           Name = c.Name,
           Type = c.ClubType!.Type,
           Description = c.Description,
           EventsNumber = c.Events == null ? 0 : c.Events.Count(),
           MembersNumber = c.UserClubs!.Count(),
            })
            .Take(4) 
            .ToListAsync();

            clubs.Reverse();

            _cache.Set(cacheKey, clubs);
         
            return clubs;
        }


        public async Task<List<ClubInfoGetDto>> GetMostActiveClubsAsync()
        {
            const string cacheKey = "MostActiveClubs";

            if (_cache.TryGetValue(cacheKey, out List<ClubInfoGetDto>? cachedClubs))
                return cachedClubs!;

            var clubs = await _db.Clubs
                .Include(c => c.ClubType)
                .Include(c => c.Events)
                .Include(c => c.UserClubs)
                .OrderByDescending(c => c.Events.Count())
                .Select(c => new ClubInfoGetDto
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Name = c.Name,
                    Type = c.ClubType!.Type,
                    Description = c.Description,
                    EventsNumber = c.Events == null ? 0 : c.Events.Count(),
                    MembersNumber = c.UserClubs!.Count(),
                })
                .Take(4)
                .ToListAsync();

            _cache.Set(cacheKey, clubs);

            return clubs;
        }



        public async Task<EventsGetDto> GetEventByIdAsync(int UserId,int EventId)
        {
            try
            {
                var Event = await _db.Events.Include(e => e.Club).ThenInclude(e=>e.UserClubs).Include(e => e.EventRegistration)
                    .Where(c => c.Id==EventId)
                    .Select(e => new EventsGetDto
                    {
                        Address = e.Address,
                        Id = e.Id,
                        Content = e.Description,
                        Date = e.Date,
                        From = e.From,
                        To = e.To,
                        Title = e.Title,
                        Views = e.Views,
                        ClubId=e.ClubId,
                        ClubName = e.Club!.Name,
                        IsStudentJoined = e.UserEvents == null ? false : e.UserEvents.Any(U => U.UserId == UserId && U.EventId == e.Id),
                        IsStudentAdmin = e.Club.UserClubs.Any(c => c.ClubId == e.ClubId && c.UserId == UserId),
                        RegistrationInfo = e.EventRegistration == null ? null : new RegistrationInfoDto
                        {
                            CurrentRegistrationsCount = e.EventRegistration.CurrentRegistrationsCount,
                            MaxRegistrationsCount = e.EventRegistration.MaxRegistrationsCount
                        },


                        IsPrivate = e.IsPrivate
                    }).FirstAsync();

                return Event;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClubInfoGetDto> GetClubByIdAsync(int UserId,int Id)
        {

            try
            {
                
                    string JoiningStatus = "NotJoined";
                    string Role = "None";
                if (UserId != -1)
                {
                    var club = await _db.UserClubs.FirstOrDefaultAsync(u => u.UserId == UserId && u.ClubId == Id);
                    if (club != null)
                    {
                        Role= club.Role;
                        JoiningStatus = "Joined";
                    }
                    else if (await _db.ClubJoiningRequests.AnyAsync(c => c.ClubId == Id && c.StudentId == UserId))
                    {
                        JoiningStatus = "Requested";
                    }
                        
                }

                return await _db.Clubs.Include(c => c.ClubType).Include(c => c.Events)
                    .Include(c => c.UserClubs).Where(c=>c.Id==Id).Select(c => new ClubInfoGetDto
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Name = c.Name,
                    Type = c.ClubType!.Type,
                    Description = c.Description,
                    OpenForRegistrations=c.OpenForRegistrations,
                    JoiningStatus = JoiningStatus,
                    StudentRole=Role,
                    TypeId=c.TypeId,
                    EventsNumber = c.Events == null ? 0 : c.Events.Count(),
                    MembersNumber = c.UserClubs!.Count(),

                }).FirstAsync();
            }
            catch
            {
                throw;
            }
        }
     

        public async Task<List<ClubUserInfoGetDto>> GetClubsMembersAsync(int ClubId)
        {
            try
            {
                return await _db.UserClubs.Include(c => c.User).Where(c => c.ClubId == ClubId).Select(c => new ClubUserInfoGetDto
                {
                    Id = c.UserId,
                    Age=c.User!.Age,
                    Degree=c.User.Degree,
                    FullName=c.User.FullName,
                    YearOfDegree=c.User.YearOfDegree,
                    ImageUrl = c.User.ImageUrl,
                    IsAdmin = c.Role == "Admin",
                    JoinedAt = c.JoinedAt

                }).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClubsGetPaginatedDto> SearchClubsPaginatedAsync(string Name,int PageNumber,int PageSize)
        {

            try
            {
                var Clubs= await _db.Clubs.Include(c => c.ClubType).Include(c => c.Events)
                    .Include(c => c.UserClubs)
                    .Where(c=>Name==""?true:c.Name.ToLower().Contains(Name.ToLower()))
                    .Select(c => new ClubInfoGetDto
                    {
                        Id = c.Id,
                        ImageUrl = c.ImageUrl,
                        Name = c.Name,
                        Type = c.ClubType!.Type,
                        Description = c.Description,
                        EventsNumber = c.Events == null ? 0 : c.Events.Count(),
                        MembersNumber = c.UserClubs!.Count(),

                    }).Skip((PageNumber-1)*PageSize).Take(PageSize).ToListAsync();

                return new ClubsGetPaginatedDto { TotalCount = Clubs.Count, Clubs = Clubs };
            }
            catch
            {
                throw;
            }
        }

    

        public async Task<List<EventsGetDto>> GetClubEventsPaginatedAsync(int UserId, int ClubId)
        {
            try
            {
                var AllEvents = _db.Events.AsQueryable();
                bool IsUserMember = await _db.UserClubs.AnyAsync(c => c.ClubId == ClubId && c.UserId == UserId);
                var Events = await _db.Events.Include(c=>c.Club).Include(e => e.EventRegistration)
                    .Where(c => c.ClubId == ClubId).Select(e => new EventsGetDto
                {
                    Address = e.Address,
                    Id = e.Id,
                    Content = e.Description,
                    Date = e.Date,
                    From = e.From,
                    To = e.To,
                    Title = e.Title,
                    ClubId=e.ClubId,
                    ClubName=e.Club!.Name,
                    Views=e.Views,
                    IsStudentJoined =UserId==-1?false:e.UserEvents.Any(U => U.UserId == UserId && U.EventId == e.Id),
                    RegistrationInfo = e.EventRegistration == null ? null : new RegistrationInfoDto
                    {
                        CurrentRegistrationsCount = e.EventRegistration.CurrentRegistrationsCount,
                        MaxRegistrationsCount = e.EventRegistration.MaxRegistrationsCount
                    },
                 

                    IsPrivate = e.IsPrivate
                })
                 .ToListAsync();


                return Events;
              
            }
            catch
            {
                throw;
            }

        }

        public async Task<EventsGetPaginatedDto> SearchEventsByNamePaginatedAsync(string Query, int PageNumber, int PageSize)
        {
            try
            {
                var AllEvents = _db.Events.AsQueryable();

                var Events = await _db.Events.Include(c => c.Club).Include(e => e.EventRegistration)
                    .Where(c => Query==""?true:c.Title.ToLower().Contains(Query.ToLower())).Select(e => new EventsGetDto
                    {
                        Address = e.Address,
                        Id = e.Id,
                        Content = e.Description,
                        Date = e.Date,
                        From = e.From,
                        To = e.To,
                        Title = e.Title,
                        ClubId = e.ClubId,
                        ClubName = e.Club!.Name,
                        Views = e.Views,
             
                        RegistrationInfo = e.EventRegistration == null ? null : new RegistrationInfoDto
                        {
                            CurrentRegistrationsCount = e.EventRegistration.CurrentRegistrationsCount,
                            MaxRegistrationsCount = e.EventRegistration.MaxRegistrationsCount
                        },


                        IsPrivate = e.IsPrivate
                    }).Skip((PageNumber - 1) * PageSize).Take(PageSize)
                 .ToListAsync();


                return new EventsGetPaginatedDto {Events=Events,TotalPages=AllEvents.Count() };

            }
            catch
            {
                throw;
            }

        }


     
        

        public async Task<List<ClubInfoGetDto>> GetClubsByTypeAsync(int TypeId,string Name)
        {
            try
            {
                return await _db.Clubs.Include(c => c.ClubType).Include(c => c.Events)
                    .Include(c => c.UserClubs).Where(c => c.TypeId == TypeId)
                    .Where(c => Name == "" ? true : c.Name.ToLower().Contains(Name.ToLower()))
                    .Select(c => new ClubInfoGetDto
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Name = c.Name,
                    Type = c.ClubType!.Type,
                    Description = c.Description,
                    EventsNumber = c.Events == null ? 0 : c.Events.Count(),
                    MembersNumber = c.UserClubs!.Count(),

                }).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ClubTypeGetDto>> GetClubsTypesAsync()
        {
            try
            {
                return await _db.ClubTypes.Include(c => c.Clubs).AsQueryable().Select(c => new ClubTypeGetDto
                {
                    Id = c.Id,
                    Type = c.Type,
                    ClubsNumber = c.Clubs == null ? 0 : c.Clubs.Count()
                }).ToListAsync();
            }
            catch
            {
                throw;
            }
        }


        public async Task ViewEventsAsync(int EventId)
        {
            try
            {
                var Event = await _db.Events.FirstAsync(e => e.Id == EventId);
                Event.Views++;
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task JoinAnEventAsync(int UserId,int EventId)
        {
            try
            {
                var Event = await _db.Events.Include(e=>e.EventRegistration).FirstAsync(e => e.Id == EventId);
                var UserEvent = new UserEvent
                {
                    EventId = Event.Id,
                    UserId = UserId
                };
               if( Event.EventRegistration!=null)
                    Event.EventRegistration.CurrentRegistrationsCount++;
                _db.Add(UserEvent);
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

    }
}
