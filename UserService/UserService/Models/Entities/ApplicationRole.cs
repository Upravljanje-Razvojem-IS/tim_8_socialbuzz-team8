using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Entities
{
    /// <summary>
    /// Entity which models roles in the system,
    /// which are used for authorization 
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Descripton of a given role
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        public bool Deleted { get; set; }
    }
}
