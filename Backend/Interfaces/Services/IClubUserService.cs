using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Services
{
    public interface IClubUserService
    {
        public  Task<ServiceResponseDto<object?>> CheckRequestClubCreationAsync(int StudentId);
        public  Task<ServiceResponseDto<object?>> JoinClubRequestAsync(ClubJoiningRequestSetDto form);
        public  Task<ServiceResponseDto<object?>> GetClubsMembersAsync(int ClubId);
        public  Task<ServiceResponseDto<object?>> GetClubEventsPaginatedAsync(int UserId,int ClubId);
        public  Task<ServiceResponseDto<object?>> RequestClubCreationAsync(int StudentId, ClubRequestCreationDto form);
        public  Task<ServiceResponseDto<object?>> SearchEventsByNameAsync( string Name, int PageNumber, int PageSize);
        public  Task<ServiceResponseDto<object?>> GetClubsByTypeAsync(int TypeId, string Name);
        public  Task<ServiceResponseDto<object?>> GetClubByIdAsync(int UserId,int ClubId);
        public  Task<ServiceResponseDto<object?>> SearchClubsPaginatedAsync(string Name,int PageNumber, int PageSize);
        public  Task<ServiceResponseDto<object?>> GetClubsTypesAsync();
        public  Task<ServiceResponseDto<object?>> GetMostActiveClubsAsync();
        public  Task<ServiceResponseDto<object?>> GetNewClubsAsync();
        public  Task<ServiceResponseDto<object?>> GetUpcomingEventsAsync(int UserId);
        public  Task<ServiceResponseDto<object?>> GetEventMemebersAsync(int EventId);
        public  Task<ServiceResponseDto<object?>> GetEventByIdAsync(int EventId, int UserId);
        public  Task<ServiceResponseDto<object?>> ViewEventsAsync(int EventId);
        public  Task<ServiceResponseDto<object?>> JoinAnEventAsync(int UserId, int EventId);


        public  Task<(byte[]? ImageData, string? ImageContentType)> GetImageAsync(int Id);
        public  Task<(byte[]? ImageData, string? ImageContentType)> GetEventImageAsync(int Id);

    }
}
