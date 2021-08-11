using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Models.Dto
{
    public class UserDetailsDto
    {
        public Guid UserDetailsID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Description { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool Deleted { get; set; }
        public string CityName { get; set; }
    }
}
