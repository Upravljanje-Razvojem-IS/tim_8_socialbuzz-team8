using ProfileService.Models;
using ProfileService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Service.ProfileServiceApi
{
    interface IProfileService
    {
        Task<List<UserDetailsDto>> GetUserDetails();
        Task<List<CorporateUserDetailsDto>> GetCorporateUserDetails();
        Task<UserDetailsDto> GetUserDetailsById(Guid id);
        Task<CorporateUserDetailsDto> GetCorporateUserDetailsById(Guid id);
        Task InsertUserDetails(UserMutationDto userDetails);
        Task UpdateUserDetails(UserMutationDto newUserDetails, Guid id);
        Task InserCorporatetUserDetails(CorporateUserDetailsMutationDto corporateUserDetails);
        Task UpdateCorporateUserDetails(CorporateUserDetails newCorporateUserDetails, Guid id);
        Task DeleteUserDetails(Guid id);
    }
}
