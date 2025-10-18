using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Services
{
    public interface IClubAdminService
    {
        public Task<ServiceResponseDto<object?>> GetCandidateApplicationAsync(int ClubId, int PageNumber, int PageSize);
        public Task<ServiceResponseDto<object?>> AcceptCandidateApplicationAsync(int ApplicationId,int UserId, int ClubId);
        public Task<ServiceResponseDto<object?>> RefuseCandidateApplicationAsync(int ApplicationId);
        public Task<ServiceResponseDto<object?>> UpdateClubInfoAsync(int UserId, ClubUpdateDto form);
        public Task<ServiceResponseDto<object?>> SetMemberAsAdminAsync(int UserId, int ClubId);
        public Task<ServiceResponseDto<object?>> RemoveMemberAsync(int UserId, int ClubId);
        public Task<ServiceResponseDto<object?>> RequestEventAsync(int Id,ClubRequestEventDto form);
        public Task<ServiceResponseDto<object?>> UpdatePostAsync(int Id,UpdatePostDto form);
        public Task<ServiceResponseDto<object?>> DeletePostAsync(int Id,int PostId);
    }

}
