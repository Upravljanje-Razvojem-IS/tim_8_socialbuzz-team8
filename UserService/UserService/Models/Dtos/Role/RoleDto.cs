using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models.Dtos.Role
{
    /// <summary>
    /// DTO that models roles in the system,
    /// which are used for authorization 
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Role unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Short Description of a given role
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicator whether role is soft deleted
        /// </summary>
        public bool Deleted { get; set; }
    }
}
