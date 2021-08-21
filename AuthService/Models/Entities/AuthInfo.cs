using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Entities
{
    /// <summary>
    /// Entity class which models information about authenticated user 
    /// </summary>
    public class AuthInfo
    {
        /// <summary>
        /// Unique identifier of a user
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Token issuing time
        /// </summary>
        [Required]
        public DateTime TimeOfIssuingToken { get; set; }

        /// <summary>
        /// Token issued for a user with UserId
        /// </summary>
        [Required]
        public string Token { get; set; }


        /// <summary>
        /// System role of the user 
        /// </summary>
        [Required]
        public string Role { get; set; }
    }
}
