using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos
{
    /// <summary>
    /// DTO for personal user account
    /// </summary>
    public class PersonalUser
    {
        /// <summary>
        /// ID of the user in database
        /// </summary>
        public Guid UserId { get; set; } //TODO: Should i omit the id property?

        /// <summary>
        /// Username of the user's account
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's birth date
        /// </summary>
        public DateTime DateOfBirth { get; set; }

    }
}
