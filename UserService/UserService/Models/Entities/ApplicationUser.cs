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

        /// <summary>
        /// Tyoe of user account that can be Personal or Corporate
        /// </summary>
        private string _userAccountType;

        public string UserAccountType
        {
            get
            {
                return _userAccountType;
            }

            set
            {
                if (value != UserAccountTypesExtensions.Personal && value != UserAccountTypesExtensions.Corporate && value != UserAccountTypesExtensions.Admin) // Check for the valid age
                {
                    throw new ArgumentException("Invalid user account type");
                }
                else
                {
                    _userAccountType = value;
                }
            }
        }

        #region constructors
        private ApplicationUser() : base()
        {
            UserAccountType = UserAccountTypesExtensions.Personal;
        }

        public ApplicationUser (string userName, string email, string phoneNumber, string userAccountType) : base(userName)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            UserAccountType = userAccountType;
        }

        public ApplicationUser(Guid id, string userName, string email, string phoneNumber, string userAccountType) : base(userName)
        {
            Id = id;
            Email = email;
            PhoneNumber = phoneNumber;
            UserAccountType = userAccountType;
        }
        #endregion

        //TODO: PASSWORD VALIDATION 
        /*By default, Identity requires that passwords contain an uppercase character, 
         * lowercase character, a digit, and a non-alphanumeric character. 
         * Passwords must be at least six characters long.*/
    }
}
