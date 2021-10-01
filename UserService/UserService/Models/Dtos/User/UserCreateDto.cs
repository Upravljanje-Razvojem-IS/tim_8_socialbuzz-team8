using ProfileService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Dtos;

namespace UserService.Models.Dtos.User
{
    public class UserCreateDto
    {
        /// <summary>
        /// Username of the user's account
        /// </summary>
        public string Username { get; set; }


        /// <summary>
        /// Users password
        /// </summary>
        public string Password { get; set; }

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
