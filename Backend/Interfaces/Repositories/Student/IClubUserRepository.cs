using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Repositories.Student
{
    public interface IClubUserRepository
    {
        public  Task RequestClubCreationAsync(int StudentId,ClubRequestCreationDto form);
       
        public  Task JoiningClubRequest(ClubJoiningRequestSetDto form);
        public  Task<List<ClubUserInfoGetDto>> GetClubsMembersAsync(int ClubId);
        public  Task<List<EventsGetDto>> GetClubEventsPaginatedAsync(int UserId, int ClubId);
        public  Task<EventsGetPaginatedDto> SearchEventsByNamePaginatedAsync(string Name,int PageNumber,int PageSize);
        public  Task<List<ClubInfoGetDto>> GetClubsByTypeAsync(int TypeId, string Name);
        public  Task<ClubInfoGetDto> GetClubByIdAsync(int UserId,int Id);
        public  Task<ClubsGetPaginatedDto> SearchClubsPaginatedAsync(string Name,int PageNumber, int PageSize);
        public  Task<List<ClubTypeGetDto>> GetClubsTypesAsync();
        public  Task<List<EventsGetDto>> GetUpcomingEventsAsync(int UserId);
        public  Task<EventsGetDto> GetEventByIdAsync(int EventId, int UserId);
        public  Task<List<ClubInfoGetDto>> GetNewClubsAsync();
        public  Task<List<ClubInfoGetDto>> GetMostActiveClubsAsync();
        public  Task ViewEventsAsync(int EventId);
        public  Task JoinAnEventAsync(int UserId, int EventId);

    }
}
