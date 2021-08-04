using ProfileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository.UserDetailsRepository
{
    public interface IUserDetailsRepository
    {
        List<UserDetails> GetUserDetails();
        List<CorporateUserDetails> GetCorporateUserDetails();
        UserDetails GetUserDetailsById(Guid id);
        CorporateUserDetails GetCorporateUserDetailsById(Guid id);
        void InsertUserDetails(UserDetails userDetails);
        void UpdateUserDetails(UserDetails userDetails);
        void InserCorporatetUserDetails(CorporateUserDetails corporateUserDetails);
        void UpdateCorporateUserDetails(CorporateUserDetails corporateUserDetails);
        UserDetails DeleteUserDetails(UserDetails userDetails);
        //public UserDetails GetUserDetailsByName(String name);
    }
}
