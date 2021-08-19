﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Models.Dto
{
    public class CorporateUserDetailsMutationDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Description { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool Deleted { get; set; }
        public Guid CityId { get; set; }
        public string Pib { get; set; }
        public string CorporationName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
