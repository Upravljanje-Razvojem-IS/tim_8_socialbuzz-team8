using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models.Dtos.Role
{
    public class RoleUpdateDto
    {
        /// <summary>
        /// Role unique identifier
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string? RoleName { get; set; }

        /// <summary>
        /// Short Description of a given role
        /// </summary>
        public string? Description { get; set; }
    }
}
