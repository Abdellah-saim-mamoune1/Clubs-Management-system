using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Repositories.Student
{
    public interface IClubAdminRepository
    {
        public Task<ClubJoiningRequestPaginatedGetDto> GetCandidatesApplicationsPaginatedAsync(int ClubId, int PageNumber, int PageSize);
        public Task UpdateClubInfoAsync(ClubUpdateDto form);
        public Task SetMemberAsAdminAsync(int UserId, int ClubId);
        public Task RemoveMemberAsync(int UserId, int ClubId);
        public Task AcceptCandidateApplicationAsync(int ApplicationId,int StudentId, int ClubId);
        public Task RefuseCandidateApplicationAsync(int ApplicationId);
        public Task RequestEventAsync(ClubRequestEventDto form);
        public Task DeletePostAsync(int Id);
        public Task UpdatePostAsync(UpdatePostDto form);
    }
}
