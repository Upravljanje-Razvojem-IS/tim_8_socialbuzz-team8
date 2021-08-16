using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Entities;

namespace UserService.Service.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser CreateCorporateUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser CreatePersonalUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(ApplicationUser role)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetCorporateUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetCorporateUsers()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetPersonalUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetPersonalUsers()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void UpdateCorporateUser(ApplicationUser oldUser, ApplicationUser newUser)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(ApplicationUser oldUser, ApplicationUser newUser)
        {
            throw new NotImplementedException();
        }
    }
}
