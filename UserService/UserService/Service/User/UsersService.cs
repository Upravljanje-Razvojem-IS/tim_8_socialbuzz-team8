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

        public void DeleteUser(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if (user == null)
            {
                throw new Exception("User with given id not found");
            }
            user.AccountIsActive = false;
            _userManager.UpdateAsync(user);
            var userProfiles = _profileService.GetUserDetails().Result;
            var profId = userProfiles.Find(u => u.Username == user.UserName).UserDetailsID;
            _profileService.DeleteUserDetails(profId).Wait();
        }

        public List<UserDto> GetAdmins()
        {
            var users = _userManager.GetUsersInRoleAsync("Administator").Result;
            if (users.Count <= 0)
            {
                var usersDto = _mapper.Map<List<UserDto>>(users);
                throw new Exception("Users with given id not found");
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
                        user.UserDetails = userProfile;
                    usersWithProfile.Add(user);
                }
                return usersWithProfile;
            }
        }

        public UserDto GetCorporateUserById(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if (user == null)
            {
                throw new Exception("User with given id not found");
            }
            var userDto = _mapper.Map<UserDto>(user);
            var userProfiles = _profileService.GetUserDetails().Result;
            var profId = userProfiles.Find(u => u.Username == user.UserName).UserDetailsID;
            var userProfile = _profileService.GetCorporateUserDetailsById(profId).Result;
            userDto.CorporateUserDetailsDto = userProfile;
            return userDto;
        }

        public List<UserDto> GetCorporateUsers()
        {
            var users = _userManager.GetUsersInRoleAsync("RegularUser").Result;
            if (users.Count <= 0)
            {
                var usersDto = _mapper.Map<List<UserDto>>(users);
                throw new Exception("Users with given id not found");
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
                        usersWithProfile.Add(user);
                    }
                }
                return usersWithProfile;
            }
        }

        public UserDto GetPersonalUserById(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if (user == null)
            {
                throw new Exception("Personal User with given id not found");
            }
            var userDto = _mapper.Map<UserDto>(user);
            var userProfiles = _profileService.GetUserDetails().Result;
            var profId = userProfiles.Find(u => u.Username == user.UserName).UserDetailsID;
            var userProfile = _profileService.GetUserDetailsById(profId).Result;
            var userProfileC = _profileService.GetCorporateUserDetailsById(profId).Result;
            if(userProfile != null && userProfileC == null)
            {
                userDto.UserDetails = userProfile;
                return userDto;
            }
            else
            {
                throw new Exception("Personal User with given id not found");

            }
        }

        public List<UserDto> GetPersonalUsers()
        {
            var users = _userManager.GetUsersInRoleAsync("RegularUser").Result;
            if (users.Count <= 0)
            {
                var usersDto = _mapper.Map<List<UserDto>>(users);
                throw new Exception("Users with given id not found");
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
                throw new Exception("Personal User with given id not found");
            }
            var userDto = _mapper.Map<UserDto>(user);
            var userProfiles = _profileService.GetUserDetails().Result;
            var profId = userProfiles.Find(u => u.Username == user.UserName).UserDetailsID;
            var userProfile = _profileService.GetUserDetailsById(profId).Result;
            userDto.UserDetails = userProfile;
            return userDto;
        }

        public List<UserDto> GetUsers()
        {
            var users = _userManager.GetUsersInRoleAsync("RegularUser").Result;
            if (users.Count <= 0)
            {
                var usersDto = _mapper.Map<List<UserDto>>(users);
                throw new Exception("Users with given id not found");
            }
            else
            {
                var userDtos = _mapper.Map<List<UserDto>>(users);
                var userProfiles = _profileService.GetUserDetails().Result;
                List<UserDto> usersWithProfile = new List<UserDto>();
                foreach (var user in userDtos)
                {
                    var userProfile = userProfiles.Find(r => r.Username == user.Username);
                    if(userProfile != null)
                    user.UserDetails = userProfile;
                    usersWithProfile.Add(user);
                }
                return usersWithProfile;
            }
        }

        public void UpdateCorporateUser(ApplicationUser oldUser, ApplicationUser newUser, CorporateUserDetailsMutationDto corporateProfile, string? oldPassword, string? newPassword)
        {
            oldUser.Email = newUser.Email;
            oldUser.UserName = newUser.UserName;
            IdentityResult result = _userManager.UpdateAsync(oldUser).Result;
            if (result.Succeeded)
            {
                if(oldPassword != null && newPassword != null)
                {
                    var passChangeRes = _userManager.ChangePasswordAsync(oldUser, oldPassword, newPassword).Result;
                    if(passChangeRes.Succeeded)
                    {
                        var userProfiles = _profileService.GetUserDetails().Result;
                        var profId = userProfiles.Find(u => u.Username == oldUser.UserName).UserDetailsID;
                        _profileService.UpdateCorporateUserDetails(corporateProfile, profId);
                    }
                }
                else
                {
                    var userProfiles = _profileService.GetUserDetails().Result;
                    var profId = userProfiles.Find(u => u.Username == oldUser.UserName).UserDetailsID;
                    _profileService.UpdateCorporateUserDetails(corporateProfile, profId);
                }
            }
            else
            {
                throw new Exception(result.Errors.ToList()[0].Description);

            }
        }

        public void UpdateUser(ApplicationUser oldUser, ApplicationUser newUser, UserMutationDto userProfile, string? oldPassword, string? newPassword)
        {
            oldUser.Email = newUser.Email;
            oldUser.UserName = newUser.UserName;
            IdentityResult result = _userManager.UpdateAsync(oldUser).Result;
            if (result.Succeeded)
            {
                if (oldPassword != null && newPassword != null)
                {
                    var passChangeRes = _userManager.ChangePasswordAsync(oldUser, oldPassword, newPassword).Result;
                    if (passChangeRes.Succeeded)
                    {
                        var userProfiles = _profileService.GetUserDetails().Result;
                        var profId = userProfiles.Find(u => u.Username == oldUser.UserName).UserDetailsID;
                        _profileService.UpdateUserDetails(userProfile, profId);
                    }
                }
                else
                {
                    var userProfiles = _profileService.GetUserDetails().Result;
                    var profId = userProfiles.Find(u => u.Username == oldUser.UserName).UserDetailsID;
                    _profileService.UpdateUserDetails(userProfile, profId);
                }
            }
            else
            {
                throw new Exception(result.Errors.ToList()[0].Description);

            }
        }
    }
}
