using ProfileService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos
{
    /// <summary>
    /// DTO that models all users accounts with basic info
    /// for internal use only
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// ID of the user in database
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Username of the user's account
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User's role in the system 
        /// used for authorization
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Type of user's account 
        /// (personal | corporation | admin)
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// Boolean value which indicates if user's account
        /// is active
        /// </summary>
        public Boolean IsActive { get; set; }


        /// <summary>
        /// Model of personal user profile info
        /// </summary>
        public UserMutationDto? UserDetails { get; set; }

        /// <summary>
        /// Model of corporate user profile info
        /// </summary>
        public CorporateUserDetailsMutationDto? CorporateUserDetailsDto { get; set; }
    }
}
