using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models.Dtos.Role
{
    public class RoleCreateDto
    {
        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Short Description of a given role
        /// </summary>
        public string Description { get; set; }
    }
}
