using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityService.Models.DTO
{
    public class UserDTO
    {
        //user mock dto
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
    }
}