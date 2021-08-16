using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Entities
{
    /// <summary>
    /// Entity class which models users in the system
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Indicator whether the user account is active or disabled
        /// </summary>
        public bool AccountIsActive { get; set; } = true;

        //TODO: PASSWORD VALIDATION 
        /*By default, Identity requires that passwords contain an uppercase character, 
         * lowercase character, a digit, and a non-alphanumeric character. 
         * Passwords must be at least six characters long.*/
    }
}
