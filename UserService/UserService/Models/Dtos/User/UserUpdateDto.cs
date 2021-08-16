using ProfileService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos

{
    public enum UserType { 
    Admin,
    Personal,
    Corporate
    }

    public enum RoleType
    {
        Admin,
        ReqularUser
    }


    /// <summary>
    /// DTO for updating  user account (basic info)
    /// used mostly for updating admin users
    /// </summary>
    public class UserUpdateDto
    {
        /// <summary>
        /// Username of the user's account
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string Password { get; set; }

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
        public string UserType { get; set; } //TODO: Delete this property if user cannot change account type


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
