using ProfileService.Models;
using ProfileService.Repository.UserDetailsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Service.UserDetailsService
{
    public class UserDetailsService : IUserDetailsService
    {
        private IUserDetailsRepository _userDetailsRepository;

        public UserDetailsService(IUserDetailsRepository userDetailsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
        }
        public UserDetails DeleteUserDetails(Guid id)
        {
            UserDetails userDetails = _userDetailsRepository.GetUserDetailsById(id);
            userDetails.Deleted = true;
            return _userDetailsRepository.DeleteUserDetails(userDetails);
        }

        public List<CorporateUserDetails> GetCorporateUserDetails()
        {
           return  _userDetailsRepository.GetCorporateUserDetails();
        }

        public CorporateUserDetails GetCorporateUserDetailsById(Guid id)
        {
            return _userDetailsRepository.GetCorporateUserDetailsById(id);
        }

        public List<UserDetails> GetUserDetails()
        {
            return _userDetailsRepository.GetUserDetails();
        }

        public UserDetails GetUserDetailsById(Guid id)
        {
            return _userDetailsRepository.GetUserDetailsById(id);
        }

        public void InserCorporatetUserDetails(CorporateUserDetails corporateUserDetails)
        {
            _userDetailsRepository.InserCorporatetUserDetails(corporateUserDetails);
        }

        public void InsertUserDetails(UserDetails userDetails)
        {
            _userDetailsRepository.InsertUserDetails(userDetails);
        }

        public void UpdateCorporateUserDetails(CorporateUserDetails oldCorporateUserDetails, CorporateUserDetails newCorporateUserDetails)
        {
            oldCorporateUserDetails.Deleted = newCorporateUserDetails.Deleted;
            oldCorporateUserDetails.Description = newCorporateUserDetails.Description;
            oldCorporateUserDetails.DateOfBirth = newCorporateUserDetails.DateOfBirth;
            oldCorporateUserDetails.CityId = newCorporateUserDetails.CityId;
            oldCorporateUserDetails.LastName = newCorporateUserDetails.LastName;
            oldCorporateUserDetails.Name = newCorporateUserDetails.Name;
            oldCorporateUserDetails.ProfilePicture = newCorporateUserDetails.ProfilePicture;
            oldCorporateUserDetails.Adress = newCorporateUserDetails.Adress;
            oldCorporateUserDetails.CorporationName = newCorporateUserDetails.CorporationName;
            oldCorporateUserDetails.Pib = newCorporateUserDetails.Pib;
            oldCorporateUserDetails.Username = newCorporateUserDetails.Username;
            oldCorporateUserDetails.Adress = newCorporateUserDetails.Adress;
            _userDetailsRepository.UpdateCorporateUserDetails(oldCorporateUserDetails);
        }

        public void UpdateUserDetails(UserDetails oldUserDetails, UserDetails newUserDetails)
        {
            oldUserDetails.Deleted = newUserDetails.Deleted;
            oldUserDetails.Description = newUserDetails.Description;
            oldUserDetails.DateOfBirth = newUserDetails.DateOfBirth;
            oldUserDetails.CityId = newUserDetails.CityId;
            oldUserDetails.LastName = newUserDetails.LastName;
            oldUserDetails.Name = newUserDetails.Name;
            oldUserDetails.ProfilePicture = newUserDetails.ProfilePicture;
            oldUserDetails.Username = newUserDetails.Username;
            oldUserDetails.Adress = newUserDetails.Adress;
            _userDetailsRepository.UpdateUserDetails(oldUserDetails);
        }
    }
}
