using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos
{
    public class CorporateUserUpdateDto
    {

        /// <summary>
        /// ID of the user in database
        /// </summary>
        public Guid UserId { get; set; } 

        /// <summary>
        /// Username of the user's account
        /// </summary>
        public string? Username { get; set; } //TODO: Check whether username and rolename are allowed to be null

        /// <summary>
        /// User's password
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Users telephone
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// Coropration name
        /// </summary>
        public string? CorporationName { get; set; }

        /// <summary>
        /// Copropration tax identification number
        /// </summary>
        public string? Tin { get; set; }

        /// <summary>
        /// Name of the owner of corporation
        /// </summary>
        public string? OwnerName { get; set; }

        /// <summary>
        /// Last name of the owner of corporation
        /// </summary>
        public string? OwnersLastName { get; set; }

        /// <summary>
        /// Date on which the corporation was established
        /// </summary>
        public DateTime? DateOfEstablisment { get; set; }

    }
}
