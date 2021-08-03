using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Entity.MockDto
{
    public class UserDto
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        /// 
        public string Name { get; set; }
        /// <summary>
        /// Last name of user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set;}

        /// <summary>
        /// User telephone
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// User date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// User description
        /// </summary>
        public string Description { get; set; }
        
    }
}
