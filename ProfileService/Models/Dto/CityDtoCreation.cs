using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Models.Dto
{
    public class CityDtoCreation
    {
        public string CityName { get; set; }
        public bool Deleted { get; set; }
        public Guid CountryId { get; set; }
    }
}
