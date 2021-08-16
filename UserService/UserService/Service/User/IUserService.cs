using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Entities;

namespace UserService.Service.User
{
    public interface IUserService
    {
        List<ApplicationUser> GetUsers();
        List<ApplicationUser> GetPersonalUsers();
        List<ApplicationUser> GetCorporateUsers();
        ApplicationUser GetUserById(Guid id);
        ApplicationUser GetPersonalUserById(Guid id);
        ApplicationUser GetCorporateUserById(Guid id);
        ApplicationUser CreatePersonalUser(ApplicationUser user);
        ApplicationUser CreateCorporateUser(ApplicationUser user);
        void UpdateUser(ApplicationUser oldUser, ApplicationUser newUser);
        void UpdateCorporateUser(ApplicationUser oldUser, ApplicationUser newUser);
        void DeleteUser(ApplicationUser role);
    }
}
