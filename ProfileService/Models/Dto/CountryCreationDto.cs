using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Models.Dto
{
    public class CountryCreationDto
    {
        public string CountryName { get; set; }
        public bool Deleted { get; set; }
    }
}
