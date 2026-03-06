using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Interfaces.Repositories.Student;
using EventsManagement.Interfaces.Services;
using Microsoft.EntityFrameworkCore;




namespace EventsManagement.Services.Club
{
    public class ClubUserService(IClubUserRepository _ClubUserRepository,AppDbContext _db) : IClubUserService
    {

        public async Task<ServiceResponseDto<object?>> RequestClubCreationAsync( int StudentId, ClubRequestCreationDto form)
        {

          
                if(await _db.RequestedClubs.AnyAsync(r => r.StudentId == StudentId))
                {
                    return new ServiceResponseDto<object?> { Status = 400 };
                }

                await _ClubUserRepository.RequestClubCreationAsync(StudentId,form);
                return new ServiceResponseDto<object?> { Status = 200 };
          

        }

        public async Task<ServiceResponseDto<object?>> CheckRequestClubCreationAsync(int StudentId)
        {

      
                if (await _db.RequestedClubs.AnyAsync(r => r.StudentId == StudentId))
                {
                    return new ServiceResponseDto<object?> { Status = 200,Data=true };
                }

                return new ServiceResponseDto<object?> { Status = 200,Data=false };
          

        }
        public async Task<ServiceResponseDto<object?>> JoinClubRequestAsync(ClubJoiningRequestSetDto form)
        {

                await _ClubUserRepository.JoiningClubRequest(form);
                return new ServiceResponseDto<object?> { Status = 200 };
      
        }
        public async Task<ServiceResponseDto<object?>> GetClubsMembersAsync(int ClubId)
        {

                var data = await _ClubUserRepository.GetClubsMembersAsync(ClubId);
                return new ServiceResponseDto<object?> { Data = data, Status = 200 };
        
        }

        public async Task<ServiceResponseDto<object?>> GetClubByIdAsync(int UserId,int ClubId)
        {

   
                var data = await _ClubUserRepository.GetClubByIdAsync(UserId,ClubId);
                return new ServiceResponseDto<object?> { Data = data, Status = 200 };

        }
        public async Task<ServiceResponseDto<object?>> GetClubEventsPaginatedAsync(int UserId,int ClubId)
        {

        
                var data = await _ClubUserRepository.GetClubEventsPaginatedAsync(UserId, ClubId);
                return new ServiceResponseDto<object?> { Data = data, Status = 200 };

        }

        public async Task<ServiceResponseDto<object?>> SearchClubsPaginatedAsync(string Name,int PageNumber, int PageSize)
        {

 
                var data = await _ClubUserRepository.SearchClubsPaginatedAsync(Name,PageNumber, PageSize);
                return new ServiceResponseDto<object?> { Data = data, Status = 200 };
   
        }


        public async Task<ServiceResponseDto<object?>> SearchEventsByNameAsync( string Name,int PageNumber,int PageSize)
        {

        
                var data = await _ClubUserRepository.SearchEventsByNamePaginatedAsync( Name,PageNumber,PageSize);
                return new ServiceResponseDto<object?> { Data = data, Status = 200 };
     

        }

      
        public async Task<ServiceResponseDto<object?>> GetEventByIdAsync(int UserId,int EventId)
        {

         
                var data = await _ClubUserRepository.GetEventByIdAsync(UserId,EventId);
                return new ServiceResponseDto<object?> { Data = data, Status = 200 };
         

        }


        public async Task<ServiceResponseDto<object?>> GetEventMemebersAsync(int EventId)
        {

                var data = await _ClubUserRepository.GetEventMembersAsync(EventId);
                return new ServiceResponseDto<object?> { Data = data, Status = 200 };
        }

        public async Task<ServiceResponseDto<object?>> GetUpcomingEventsAsync(int UserId)
        {

        
                var data = await _ClubUserRepository.GetUpcomingEventsAsync(UserId);
                return new ServiceResponseDto<object?> { Data=data,Status = 200 };
         
        }

     
        public async Task<ServiceResponseDto<object?>> GetNewClubsAsync()
        {

          
                var data = await _ClubUserRepository.GetNewClubsAsync();
                return new ServiceResponseDto<object?> { Data = data, Status = 200 };
      
        }

        public async Task<ServiceResponseDto<object?>> GetMostActiveClubsAsync()
        {

        
                var data = await _ClubUserRepository.GetMostActiveClubsAsync();
                return new ServiceResponseDto<object?> { Data = data, Status = 200 };
         

        }
        public async Task<ServiceResponseDto<object?>> GetClubsByTypeAsync(int TypeId,string Name)
        {

          
                var data = await _ClubUserRepository.GetClubsByTypeAsync(TypeId,Name);
                return new ServiceResponseDto<object?> { Data = data, Status = 200 };
      

        }

        public async Task<ServiceResponseDto<object?>> GetClubsTypesAsync()
        {

         
                var data = await _ClubUserRepository.GetClubsTypesAsync();
                return new ServiceResponseDto<object?> { Data = data, Status = 200 };
         

        }

        public async Task<ServiceResponseDto<object?>> ViewEventsAsync(int EventId)
        {

           
                 await _ClubUserRepository.ViewEventsAsync(EventId);
                 return new ServiceResponseDto<object?> { Status = 200 };
      
        }

        public async Task<ServiceResponseDto<object?>> JoinAnEventAsync(int UserId, int EventId)
        {

       
                var Event=await _db.Events.AsQueryable().Include(c => c.Club).ThenInclude(c => c!.UserClubs).FirstAsync(c => c.Id == EventId);
                if(Event.IsPrivate&&!Event.Club!.UserClubs.Any(u=>u.UserId==UserId))
                    return new ServiceResponseDto<object?> { Status = 401 };


                await _ClubUserRepository.JoinAnEventAsync(UserId, EventId);
                return new ServiceResponseDto<object?> { Status = 200 };
          

        }



        public async Task<(byte[]? ImageData, string? ImageContentType)> GetImageAsync(int Id)
        {

            return await _ClubUserRepository.GetImageAsync(Id);

        }


        public async Task<(byte[]? ImageData, string? ImageContentType)> GetEventImageAsync(int Id)
        {

            return await _ClubUserRepository.GetEventImageAsync(Id);

        }
    }
}
