using ProfileService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Dtos;
using UserService.Entities;

namespace UserService.Service.User
{
    public interface IUsersService
    {
        List<UserDto> GetUsers();
        List<UserDto> GetPersonalUsers();
        List<UserDto> GetAdmins();
        List<UserDto> GetCorporateUsers();
        UserDto GetUserById(Guid id);
        UserDto GetPersonalUserById(Guid id);
        UserDto GetCorporateUserById(Guid id);
        ApplicationUser CreatePersonalUser(ApplicationUser user, UserMutationDto personalUserProfile, string password);
        ApplicationUser CreateAdminUser(ApplicationUser user, UserMutationDto personalUserProfile, string password);
        ApplicationUser CreateCorporateUser(ApplicationUser user, CorporateUserDetailsMutationDto corporateUserProfile, string password);
        void UpdateUser(ApplicationUser oldUser, ApplicationUser newUser, UserMutationDto userProfile, string? oldPassword, string? newPassword);
        void UpdateCorporateUser(ApplicationUser oldUser, ApplicationUser newUser, CorporateUserDetailsMutationDto corporateProfile, string? oldPassword, string? newPassword);
        void DeleteUser(Guid id);
    }
}
