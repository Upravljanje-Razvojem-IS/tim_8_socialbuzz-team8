using ProfileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Service.UserDetailsService
{
    public interface IUserDetailsService
    {
        List<UserDetails> GetUserDetails();
        List<CorporateUserDetails> GetCorporateUserDetails();
        UserDetails GetUserDetailsById(Guid id);
        CorporateUserDetails GetCorporateUserDetailsById(Guid id);
        void InsertUserDetails(UserDetails userDetails);
        void UpdateUserDetails(UserDetails oldUserDetails, UserDetails newUserDetails);
        void InserCorporatetUserDetails(CorporateUserDetails corporateUserDetails);
        void UpdateCorporateUserDetails(CorporateUserDetails corporateUserDetails);
        UserDetails DeleteUserDetails(Guid id);

    }
}
