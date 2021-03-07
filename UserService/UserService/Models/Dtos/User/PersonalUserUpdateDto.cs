using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos
{ 
    public class PersonalUserUpdateDto
    {
        /// <summary>
        /// ID of the user in database
        /// </summary>
        public Guid UserId { get; set; } 

        /// <summary>
        /// User's username
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// User's birth date
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
    }
}
