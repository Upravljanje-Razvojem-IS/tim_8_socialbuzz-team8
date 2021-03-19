using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos
{
    /// <summary>
    /// DTO for creating a basic user account
    /// used mostly for creating admin users
    /// </summary>
    public class UserRegistrationDto
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
        public string UserType { get; set; }

     
    }
}
