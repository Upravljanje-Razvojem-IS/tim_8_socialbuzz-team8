using ITSolutionsCompanyV1.Repositories;
using ProfileService.Data;
using ProfileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository.UserDetailsRepository
{
    public class UserDetailsRepository : GenericRepository<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepository(ProfileDbContext dataContext) : base(dataContext)
        {
        }
        public UserDetails DeleteUserDetails(UserDetails userDetails)
        {
            Update(userDetails);
            return userDetails;
        }

        public List<CorporateUserDetails> GetCorporateUserDetails()
        {
            return Query.OfType<CorporateUserDetails>().ToList();
        }

        public CorporateUserDetails GetCorporateUserDetailsById(Guid id)
        {
            var b = Query.OfType<CorporateUserDetails>()
                .Where(cud => cud.UserDetailsID == id);
            if(!b.Any())
            {
                return null;
            }
            else
            {
                return b.First(); 
            }
        }

        public List<UserDetails> GetUserDetails()
        {
            return FindAll();
        }

        public UserDetails GetUserDetailsById(Guid id)
        {
            return FindById(id);
        }

        public void InserCorporatetUserDetails(CorporateUserDetails corporateUserDetails)
        {
            Insert(corporateUserDetails);
        }

        public void InsertUserDetails(UserDetails userDetails)
        {
            Insert(userDetails);
        }

        public void UpdateCorporateUserDetails(CorporateUserDetails corporateUserDetails)
        {
            Update(corporateUserDetails);
        }

        public void UpdateUserDetails(UserDetails userDetails)
        {
            Update(userDetails);
        }
    }
}
