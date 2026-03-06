using EventsManagement.Data;
using EventsManagement.Dtos;
using EventsManagement.Interfaces.Repositories.Student;
using EventsManagement.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.Services.Club
{
    public class ClubAdminService(
        IClubAdminRepository _IClubAdminRepository,
        AppDbContext _db) : IClubAdminService
    {

        public async Task<ServiceResponseDto<object?>> UpdateClubInfoAsync(int UserId,ClubUpdateDto form)
        {

        
                if (!await IsAdmin(UserId, form.ClubId))
                    return new ServiceResponseDto<object?> { Status = 401 };

                await _IClubAdminRepository.UpdateClubInfoAsync(form);
                return new ServiceResponseDto<object?> { Status = 200 };
         

        }

        public async Task<ServiceResponseDto<object?>> SetMemberAsAdminAsync(int UserId, int ClubId)
        {

           
                await _IClubAdminRepository.SetMemberAsAdminAsync(UserId, ClubId);
                return new ServiceResponseDto<object?> { Status = 200 };
 
        }

        public async Task<ServiceResponseDto<object?>> RemoveMemberAsync(int UserId, int ClubId)
        {

           
                await _IClubAdminRepository.RemoveMemberAsync(UserId, ClubId);
                return new ServiceResponseDto<object?> { Status = 200 };
      

        }

        public async Task<ServiceResponseDto<object?>> GetCandidateApplicationAsync( int ClubId,int PageNumber,int PageSize)
        {

       
               var data= await _IClubAdminRepository.GetCandidatesApplicationsPaginatedAsync(ClubId,PageNumber,PageSize);
                return new ServiceResponseDto<object?> { Status = 200,Data=data };
     
        }
        public async Task<ServiceResponseDto<object?>> AcceptCandidateApplicationAsync(int ApplicationId,int UserId, int ClubId)
        {

        
                await _IClubAdminRepository.AcceptCandidateApplicationAsync(ApplicationId,UserId, ClubId);
                return new ServiceResponseDto<object?> { Status = 200 };
       
        }

        public async Task<ServiceResponseDto<object?>> RefuseCandidateApplicationAsync(int ApplicationId)
        {

       
                await _IClubAdminRepository.RefuseCandidateApplicationAsync(ApplicationId);
                return new ServiceResponseDto<object?> { Status = 200 };
       

        }

        public async Task<ServiceResponseDto<object?>> RequestEventAsync(int Id,ClubRequestEventDto form)
        {
    
                if(!await IsAdmin(Id,form.ClubId))
                    return new ServiceResponseDto<object?> { Status = 401 };

                await _IClubAdminRepository.RequestEventAsync(form);
                return new ServiceResponseDto<object?> { Status = 200 };
  
        }
        public async Task<ServiceResponseDto<object?>> UpdatePostAsync(int Id,UpdatePostDto form)
        {

         
                if (!await IsAdmin(Id, form.Id))
                    return new ServiceResponseDto<object?> { Status = 401 };

                await _IClubAdminRepository.UpdatePostAsync(form);
                return new ServiceResponseDto<object?> { Status = 200 };
    

        }

        public async Task<ServiceResponseDto<object?>> DeletePostAsync(int Id,int PostId)
        {

       
                if (!await IsAdmin(Id, PostId))
                    return new ServiceResponseDto<object?> { Status = 401 };

                await _IClubAdminRepository.DeletePostAsync(PostId);
                return new ServiceResponseDto<object?> { Status = 200 };
         


        }

        private async Task<bool> IsAdmin(int AdminId,int ClubId)
        {
          return await _db.UserClubs.AnyAsync(u => u.ClubId == ClubId&&u.UserId==AdminId&&u.Role=="Admin");

        }
    }
}
