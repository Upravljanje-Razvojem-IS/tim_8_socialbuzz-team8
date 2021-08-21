using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProfileService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Dtos;
using UserService.Entities;
using UserService.Service.ProfileServiceApi;

namespace UserService.Service.User
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfilesServiceApi _profileService;
        private readonly IMapper _mapper;

        public UsersService(UserManager<ApplicationUser> userManager, IProfilesServiceApi profileService, IMapper mapper)
        {
            _userManager = userManager;
            _profileService = profileService;
            _mapper = mapper;
        }

        public ApplicationUser CreateAdminUser(ApplicationUser user, UserMutationDto personalUserProfile, string password)
        {
            user.Id = Guid.NewGuid();
            user.AccountIsActive = true;
            IdentityResult result = _userManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Administrator").Wait();
                personalUserProfile.Username = user.UserName;
                _profileService.InsertUserDetails(personalUserProfile).Wait();
                return user;
            }
            else
            {
                throw new Exception(result.Errors.ToList()[0].Description);

            }
        }

        //TODO: create admin

        public ApplicationUser CreateCorporateUser(ApplicationUser user, CorporateUserDetailsMutationDto corporateUserProfile, string password)
        {
            user.Id = Guid.NewGuid();
            user.AccountIsActive = true;
            IdentityResult result = _userManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "RegularUser").Wait();
                corporateUserProfile.Username = user.UserName;
                _profileService.InserCorporatetUserDetails(corporateUserProfile).Wait();
                return user;
            }
            else
            {
                throw new Exception(result.Errors.ToList()[0].Description);

            }
        }

        public ApplicationUser CreatePersonalUser(ApplicationUser user, UserMutationDto personalUserProfile, string password)
        {
            user.Id = Guid.NewGuid();
            user.AccountIsActive = true;
            IdentityResult result = _userManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "RegularUser").Wait();
                personalUserProfile.Username = user.UserName;
                _profileService.InsertUserDetails(personalUserProfile).Wait();
                return user;
            }
            else
            {
                throw new Exception(result.Errors.ToList()[0].Description);

            }
        }

        public void DeleteUser(Guid id, string token)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if (user == null)
            {
                throw new Exception("User with given id not found");
            }
            user.AccountIsActive = false;
            _userManager.UpdateAsync(user);
            var userProfiles = _profileService.GetUserDetails().Result;
            var prof= userProfiles.Find(u => u.Username == user.UserName);
            if(prof!=null)
                _profileService.DeleteUserDetails(prof.UserDetailsID, token).Wait();
        }

        public List<UserDto> GetAdmins()
        {
            var users = _userManager.GetUsersInRoleAsync("Administrator").Result;
            if (users.Count <= 0)
            {
                var usersDto = _mapper.Map<List<UserDto>>(users);
                throw new Exception("Users with given role not found");
            }
            else
            {
                var userDtos = _mapper.Map<List<UserDto>>(users);
                var userProfiles = _profileService.GetUserDetails().Result;
                List<UserDto> usersWithProfile = new List<UserDto>();
                foreach (var user in userDtos)
                {
                    var userProfile = userProfiles.Find(r => r.Username == user.Username);
                    if (userProfile != null)
                    {
                        user.UserDetails = userProfile;
                        user.Role = "Administrator";
                        usersWithProfile.Add(user);

                    }
                }
                return usersWithProfile;
            }
        }

        private UserDto GetCorporateUserById(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if (user == null)
            {
                return null;
            }
            var userDto = _mapper.Map<UserDto>(user);
            var userProfiles = _profileService.GetUserDetails().Result;
            var prof = userProfiles.Find(u => u.Username == user.UserName);
            if (prof != null)
            {
                var userProfile = _profileService.GetCorporateUserDetailsById(prof.UserDetailsID).Result;
                if (userProfile == null)
                {
                    return null;
                }
                userDto.Role = "RegularUser";
                userDto.CorporateUserDetailsDto = userProfile;
                return userDto;
            }
            else
            {
                return null;
            }
        }

        public List<UserDto> GetCorporateUsers()
        {
            var users = _userManager.GetUsersInRoleAsync("RegularUser").Result;
            if (users.Count <= 0)
            {
                var usersDto = _mapper.Map<List<UserDto>>(users);
                throw new Exception("Users with given role not found");
            }
            else
            {
                var userDtos = _mapper.Map<List<UserDto>>(users);
                var userProfiles = _profileService.GetCorporateUserDetails().Result;
                List<UserDto> usersWithProfile = new List<UserDto>();
                foreach (var user in userDtos)
                {
                    var userProfile = userProfiles.Find(r => r.Username == user.Username);
                    if(userProfile != null)
                    {
                        user.CorporateUserDetailsDto = userProfile;
                        user.Role = "RegularUser";
                        usersWithProfile.Add(user);
                    }
                }
                return usersWithProfile;
            }
        }

        private UserDto GetPersonalUserById(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if (user == null)
            {
                return null;
            }
            var userDto = _mapper.Map<UserDto>(user);
            var userProfiles = _profileService.GetUserDetails().Result;
            var prof = userProfiles.Find(u => u.Username == user.UserName);
            if(prof == null)
            {
                return null;
            }
            var userProfile = _profileService.GetUserDetailsById(prof.UserDetailsID).Result;
            var userProfileC = _profileService.GetCorporateUserDetailsById(prof.UserDetailsID).Result;
            if(userProfile != null && userProfileC == null)
            {
                userDto.UserDetails = userProfile;
                userDto.Role = "RegularUser";
                return userDto;
            }
            else
            {
                return null;
            }
        }

        private UserDto GetAdminById(Guid id)
        {
            var admins = this.GetAdmins();
            var admin = this.GetAdmins().Find(a => a.Id == id);
            return admin;
        }

        public List<UserDto> GetPersonalUsers()
        {
            var users = _userManager.GetUsersInRoleAsync("RegularUser").Result;
            if (users.Count <= 0)
            {
                var usersDto = _mapper.Map<List<UserDto>>(users);
                throw new Exception("Users with given role not found");
            }
            else
            {
                var userDtos = _mapper.Map<List<UserDto>>(users);
                var userProfiles = _profileService.GetUserDetails().Result;
                var userProfilesC = _profileService.GetCorporateUserDetails().Result;
                List<UserDto> usersWithProfile = new List<UserDto>();
                foreach (var user in userDtos)
                {
                    var userProfile = userProfiles.Find(r => r.Username == user.Username);
                    var userProfileC = userProfilesC.Find(r => r.Username == user.Username);
                    if (userProfile != null && userProfileC == null)
                    {
                        user.UserDetails = userProfile;
                        user.Role = "RegularUser";
                        usersWithProfile.Add(user);
                    }
                }
                return usersWithProfile;
            }
        }

        public UserDto GetUserById(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if (user == null)
            {
                return null;
            }
            var corporateProfile = this.GetCorporateUserById(id);
            var personalProfile = this.GetPersonalUserById(id);
            var admin = this.GetAdminById(id);
            if(admin != null)
            {
                return admin;
            }
            else if(personalProfile != null)
            {
                return personalProfile;
            }
            else
            {
                return corporateProfile;
            }
        }

        public List<UserDto> GetUsers()
        {
            List<ApplicationUser> users = (List<ApplicationUser>) _userManager.GetUsersInRoleAsync("RegularUser").Result;
            var limit = users.Count;
            var adminUsers = _userManager.GetUsersInRoleAsync("Administrator").Result;
            users.AddRange(adminUsers);
            if (users.Count <= 0)
            {
                var usersDto = _mapper.Map<List<UserDto>>(users);
                throw new Exception("Users with given id not found");
            }
            else
            {
                var userDtos = _mapper.Map<List<UserDto>>(users);
                var userProfiles = _profileService.GetUserDetails().Result;
                var corporateProfiles = _profileService.GetCorporateUserDetails().Result;
                List<UserDto> usersWithProfile = new List<UserDto>();
                foreach (var user in userDtos)
                {
                    var userProfile = userProfiles.Find(r => r.Username == user.Username);
                    var userCorporateProfile = corporateProfiles.Find(r => r.Username == user.Username);
                    if (userCorporateProfile != null)
                    {
                        user.CorporateUserDetailsDto = userCorporateProfile;
                        user.Role = "RegularUser";
                        usersWithProfile.Add(user);
                    }
                    else if (userProfile != null)
                    {
                        user.UserDetails = userProfile;
                        if(userDtos.IndexOf(user) <= (limit - 1))
                        {
                            user.Role = "RegularUser";
                        }
                        else
                        {
                            user.Role = "Administrator";
                        }
                        usersWithProfile.Add(user);
                    }

                }
                return usersWithProfile;
            }
        }

        private void UpdateCorporateUser(ApplicationUser oldUser, ApplicationUser newUser, CorporateUserDetailsMutationDto corporateProfile, string? oldPassword, string? newPassword, string token)
        {
            var searchUserName = oldUser.UserName;
            if(oldUser.Email != newUser.Email)
                oldUser.Email = newUser.Email;
            if (oldUser.UserName != newUser.UserName)
                oldUser.UserName = newUser.UserName;

            IdentityResult result = _userManager .UpdateAsync(oldUser).Result;
            if (result.Succeeded)
            {
                if(oldPassword != null && newPassword != null)
                {
                    var passChangeRes = _userManager.ChangePasswordAsync(oldUser, oldPassword, newPassword).Result;
                    if(passChangeRes.Succeeded)
                    {
                        var userProfiles = _profileService.GetUserDetails().Result;
                        var prof = userProfiles.Find(u => u.Username == searchUserName);
                        if(prof != null )
                        {
                            corporateProfile.Username = oldUser.UserName;
                            _profileService.UpdateCorporateUserDetails(corporateProfile, prof.UserDetailsID, token);
                        }
                    }
                }
                else
                {
                    var userProfiles = _profileService.GetUserDetails().Result;
                    var prof = userProfiles.Find(u => u.Username == searchUserName);
                    if (prof != null)
                    {
                        corporateProfile.Username = oldUser.UserName;
                        _profileService.UpdateCorporateUserDetails(corporateProfile, prof.UserDetailsID, token);
                    }
                }
            }
            else
            {
                throw new Exception(result.Errors.ToList()[0].Description);

            }
        }

        private void UpdatePersonalUser(ApplicationUser oldUser, ApplicationUser newUser, UserMutationDto userProfile, string? oldPassword, string? newPassword, string token)
        {
            var searchUserName = oldUser.UserName;
            if (oldUser.Email != newUser.Email)
                oldUser.Email = newUser.Email;
            if (oldUser.UserName != newUser.UserName)
                oldUser.UserName = newUser.UserName;
            IdentityResult result = _userManager.UpdateAsync(oldUser).Result;
            if (result.Succeeded)
            {
                if (oldPassword != null && newPassword != null && oldPassword != newPassword)
                {
                    var passChangeRes = _userManager.ChangePasswordAsync(oldUser, oldPassword, newPassword).Result;
                    if (passChangeRes.Succeeded)
                    {
                        var userProfiles = _profileService.GetUserDetails().Result;
                        var prof = userProfiles.Find(u => u.Username == searchUserName);
                        if(prof != null)
                        {
                            _profileService.UpdateUserDetails(userProfile, prof.UserDetailsID, token);
                        }
                    }
                }
                else
                {
                    var userProfiles = _profileService.GetUserDetails().Result;
                    var prof = userProfiles.Find(u => u.Username == searchUserName);
                    if(prof != null)
                    {
                        userProfile.Username = oldUser.UserName;
                        _profileService.UpdateUserDetails(userProfile, prof.UserDetailsID, token);
                    }
                }
            }
            else
            {
                throw new Exception(result.Errors.ToList()[0].Description);

            }
        }

        public void UpdateUser(Guid id, UserUpdateDto userProfile, string token)
        {
            var corporateUser = this.GetCorporateUserById(id);
            var oldUser = _userManager.FindByIdAsync(id.ToString()).Result;
            if(corporateUser != null)
            {
                this.UpdateCorporateUser(
                    oldUser,
                    _mapper.Map<ApplicationUser>(userProfile),
                    userProfile.CorporateUserDetailsDto,
                    null,
                    null,
                    token
                    );
            }
            else
            {
                this.UpdatePersonalUser(
                   oldUser,
                   _mapper.Map<ApplicationUser>(userProfile),
                   userProfile.UserDetails,
                   null,
                   null,
                   token
                   );
            }
        }

    }
}
