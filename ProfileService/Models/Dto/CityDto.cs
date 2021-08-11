using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Models.Dto
{
    public class CityDto
    {
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public bool Deleted { get; set; }
        public string Country { get; set; }
    }
}
